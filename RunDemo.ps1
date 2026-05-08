# RunDemo.ps1
# Clean + restore + build + run the Quark Suite demo for browser/Codex testing.
# No dotnet watch. No double compile.

[CmdletBinding()]
param(
    [switch]$NoClean,
    [switch]$NoWait,
    [switch]$UsePackages,
    [int]$ReadyTimeoutSeconds = 180
)

$ErrorActionPreference = "Stop"

if ($PSVersionTable.PSVersion.Major -ge 7) {
    $PSNativeCommandUseErrorActionPreference = $true
}

# --- Config -----------------------------------------------------------------------

$Configuration = "DebugDemo"
$Url = "http://localhost:7040"

$UseLocalProjects = if ($UsePackages) { "false" } else { "true" }

$PidFile = Join-Path $PSScriptRoot ".Demo-pid.txt"
$OutLog = Join-Path $PSScriptRoot ".Demo-out.log"
$ErrLog = Join-Path $PSScriptRoot ".Demo-err.log"

# Quark repo
$DemoProjectDir = Join-Path $PSScriptRoot "test\Soenneker.Quark.Suite.Demo"
$DemoProject = Join-Path $DemoProjectDir "Soenneker.Quark.Suite.Demo.csproj"

$QuarkSuiteDir = Join-Path $PSScriptRoot "src\Soenneker.Quark.Suite"
$QuarkSuiteProject = Join-Path $QuarkSuiteDir "Soenneker.Quark.Suite.csproj"

# Local dependency repos used by Soenneker.Quark.Suite when UseLocalProjects=true
$BradixSuiteDir = "C:\git\Soenneker\Bradix\soenneker.bradix.suite\src\Soenneker.Bradix.Suite"
$BradixSuiteProject = Join-Path $BradixSuiteDir "Soenneker.Bradix.Suite.csproj"

$QuarkBuildersDir = "C:\git\Soenneker\Quark\soenneker.quark.builders\src\Soenneker.Quark.Builders"
$QuarkBuildersProject = Join-Path $QuarkBuildersDir "Soenneker.Quark.Builders.csproj"

$QuarkGenPresetsDir = "C:\git\Soenneker\Quark\soenneker.quark.gen.presets\src\Soenneker.Quark.Gen.Presets"
$QuarkGenPresetsProject = Join-Path $QuarkGenPresetsDir "Soenneker.Quark.Gen.Presets.csproj"

# --- Helpers ----------------------------------------------------------------------

function Write-Section {
    param([string]$Message)

    Write-Host ""
    Write-Host "=== $Message ===" -ForegroundColor Cyan
}

function Invoke-Checked {
    param(
        [Parameter(Mandatory)]
        [string]$FilePath,

        [Parameter(Mandatory)]
        [string[]]$Arguments
    )

    Write-Host ""
    Write-Host "$FilePath $($Arguments -join ' ')" -ForegroundColor DarkGray

    & $FilePath @Arguments

    if ($LASTEXITCODE -ne 0) {
        throw "$FilePath failed with exit code $LASTEXITCODE"
    }
}

function Stop-ExistingSessions {
    if (-not (Test-Path $PidFile)) {
        return
    }

    Write-Section "Stopping previous demo session"

    Get-Content $PidFile |
        Where-Object { $_ -match '^\d+$' } |
        Select-Object -Unique |
        ForEach-Object {
            $processId = [int]$_

            try {
                $process = Get-Process -Id $processId -ErrorAction SilentlyContinue

                if (-not $process) {
                    return
                }

                if ($IsWindows) {
                    Start-Process `
                        -FilePath "taskkill.exe" `
                        -ArgumentList "/PID $processId /T /F" `
                        -NoNewWindow `
                        -Wait
                }
                else {
                    Stop-Process -Id $processId -Force -ErrorAction SilentlyContinue
                }
            }
            catch {
                # Ignore stale/dead PIDs.
            }
        }

    Remove-Item $PidFile -ErrorAction SilentlyContinue
}

function Stop-LingeringDemoProcesses {
    Write-Section "Stopping lingering demo processes"

    $escapedProject = [regex]::Escape("Soenneker.Quark.Suite.Demo.csproj")
    $escapedProjectDir = [regex]::Escape($DemoProjectDir)
    $escapedUrl = [regex]::Escape($Url)

    $processes = Get-CimInstance Win32_Process -Filter "Name='dotnet.exe'" -ErrorAction SilentlyContinue |
        Where-Object {
            $commandLine = $_.CommandLine

            if ([string]::IsNullOrWhiteSpace($commandLine)) {
                return $false
            }

            return $commandLine -match $escapedProject -or
                $commandLine -match $escapedProjectDir -or
                ($commandLine -match "Microsoft\.AspNetCore\.Components\.WebAssembly\.DevServer" -and $commandLine -match $escapedUrl)
        }

    foreach ($process in $processes) {
        try {
            Write-Host "Stopping PID $($process.ProcessId): $($process.CommandLine)"
            Invoke-CimMethod -InputObject $process -MethodName Terminate | Out-Null
        }
        catch {
            Write-Host "Could not stop lingering demo PID $($process.ProcessId): $($_.Exception.Message)" -ForegroundColor Yellow
        }
    }
}

function Remove-BuildOutput {
    param([string]$ProjectDir)

    if (-not (Test-Path $ProjectDir)) {
        return
    }

    Write-Host "Cleaning: $ProjectDir"

    Remove-Item -Recurse -Force -ErrorAction SilentlyContinue `
        (Join-Path $ProjectDir "bin"),
        (Join-Path $ProjectDir "obj")
}

function Assert-FileExists {
    param([string]$Path)

    if (-not (Test-Path $Path)) {
        throw "Required file not found: $Path"
    }
}

function Test-HttpOk {
    param([string]$TargetUrl)

    try {
        $response = Invoke-WebRequest `
            -Uri $TargetUrl `
            -UseBasicParsing `
            -TimeoutSec 5 `
            -Headers @{
                "Cache-Control" = "no-cache"
                "Pragma" = "no-cache"
            }

        return $response.StatusCode -ge 200 -and $response.StatusCode -lt 300
    }
    catch {
        return $false
    }
}

function Test-JavaScriptModule {
    param([string]$TargetUrl)

    try {
        $response = Invoke-WebRequest `
            -Uri $TargetUrl `
            -UseBasicParsing `
            -TimeoutSec 5 `
            -Headers @{
                "Cache-Control" = "no-cache"
                "Pragma" = "no-cache"
            }

        if ($response.StatusCode -lt 200 -or $response.StatusCode -ge 300) {
            return $false
        }

        $content = [string]$response.Content

        if ([string]::IsNullOrWhiteSpace($content)) {
            return $false
        }

        $trimmed = $content.TrimStart()

        if ($trimmed.StartsWith("<!DOCTYPE", [StringComparison]::OrdinalIgnoreCase)) {
            return $false
        }

        if ($trimmed.StartsWith("<html", [StringComparison]::OrdinalIgnoreCase)) {
            return $false
        }

        return $true
    }
    catch {
        return $false
    }
}

function Wait-ForDemoReady {
    param(
        [string]$BaseUrl,
        [int]$TimeoutSeconds,
        [System.Diagnostics.Process]$Process
    )

    Write-Section "Waiting for demo readiness"

    $deadline = [DateTimeOffset]::UtcNow.AddSeconds($TimeoutSeconds)

    $homeUrl = "$BaseUrl/?v=$([DateTimeOffset]::UtcNow.ToUnixTimeSeconds())"
    $blazorUrl = "$BaseUrl/_framework/blazor.webassembly.js"
    $dotnetUrl = "$BaseUrl/_framework/dotnet.js"

    while ([DateTimeOffset]::UtcNow -lt $deadline) {
        if ($Process.HasExited) {
            Write-Host ""
            Write-Host "Demo process exited early with code $($Process.ExitCode)." -ForegroundColor Red

            if (Test-Path $OutLog) {
                Write-Host ""
                Write-Host "--- stdout ---" -ForegroundColor Yellow
                Get-Content $OutLog -Tail 120
            }

            if (Test-Path $ErrLog) {
                Write-Host ""
                Write-Host "--- stderr ---" -ForegroundColor Yellow
                Get-Content $ErrLog -Tail 120
            }

            throw "Demo process exited before becoming ready."
        }

        $homeReady = Test-HttpOk $homeUrl
        $blazorReady = Test-JavaScriptModule $blazorUrl
        $dotnetReady = Test-JavaScriptModule $dotnetUrl

        if ($homeReady -and $blazorReady -and $dotnetReady) {
            Write-Host "Demo is responding: $homeUrl" -ForegroundColor Green
            Write-Host "Blazor JS is responding: $blazorUrl" -ForegroundColor Green
            Write-Host "dotnet.js is responding: $dotnetUrl" -ForegroundColor Green
            return
        }

        Start-Sleep -Milliseconds 750
    }

    Write-Host ""
    Write-Host "--- stdout ---" -ForegroundColor Yellow
    if (Test-Path $OutLog) {
        Get-Content $OutLog -Tail 120
    }

    Write-Host ""
    Write-Host "--- stderr ---" -ForegroundColor Yellow
    if (Test-Path $ErrLog) {
        Get-Content $ErrLog -Tail 120
    }

    throw "Timed out waiting for demo readiness after $TimeoutSeconds seconds: $BaseUrl"
}

function Start-Demo {
    $env:ASPNETCORE_ENVIRONMENT = "Development"
    $env:DOTNET_MSBUILDARGS = "/m:1"

    Remove-Item $OutLog, $ErrLog -Force -ErrorAction SilentlyContinue

    $arguments = @(
        "run",
        "--project", "`"$DemoProject`"",
        "-c", $Configuration,
        "--no-build",
        "--no-restore",
        "--",
        "--urls", $Url
    )

    $argumentLine = $arguments -join " "

    Write-Host ""
    Write-Host "dotnet $argumentLine" -ForegroundColor DarkGray

    $process = Start-Process `
        -FilePath "dotnet.exe" `
        -ArgumentList $argumentLine `
        -WorkingDirectory $PSScriptRoot `
        -RedirectStandardOutput $OutLog `
        -RedirectStandardError $ErrLog `
        -WindowStyle Hidden `
        -PassThru

    Set-Content -Path $PidFile -Value $process.Id

    return $process
}

# --- Validate ---------------------------------------------------------------------

Assert-FileExists $DemoProject
Assert-FileExists $QuarkSuiteProject

if ($UseLocalProjects -eq "true") {
    Assert-FileExists $BradixSuiteProject
    Assert-FileExists $QuarkBuildersProject
    Assert-FileExists $QuarkGenPresetsProject
}

# --- Run --------------------------------------------------------------------------

Stop-ExistingSessions
Stop-LingeringDemoProcesses

if (-not $NoClean) {
    Write-Section "Cleaning build output"

    Remove-BuildOutput $DemoProjectDir
    Remove-BuildOutput $QuarkSuiteDir

    if ($UseLocalProjects -eq "true") {
        Remove-BuildOutput $BradixSuiteDir
        Remove-BuildOutput $QuarkBuildersDir
        Remove-BuildOutput $QuarkGenPresetsDir
    }
}
else {
    Write-Host "(Skipping clean)"
}

Write-Section "Restore full graph"

Invoke-Checked "dotnet" @(
    "restore",
    $DemoProject,
    "-p:Configuration=$Configuration",
    "-p:UseLocalProjects=$UseLocalProjects",
    "-p:ContinuousIntegrationBuild=false",
    "-p:IncludeSymbols=false",
    "-p:DebugSymbols=false",
    "-p:DebugType=none",
    "-p:BlazorCacheBootResources=false",
    "-r", "browser-wasm"
)

Write-Section "Build full graph"

Invoke-Checked "dotnet" @(
    "build",
    $DemoProject,
    "-c", $Configuration,
    "--no-restore",
    "-p:UseLocalProjects=$UseLocalProjects",
    "-p:ContinuousIntegrationBuild=false",
    "-p:IncludeSymbols=false",
    "-p:DebugSymbols=false",
    "-p:DebugType=none",
    "-p:BlazorCacheBootResources=false"
)

Write-Section "Starting Demo"

Write-Host "Configuration: $Configuration"
Write-Host "UseLocalProjects: $UseLocalProjects"

$process = Start-Demo

if (-not $NoWait) {
    Wait-ForDemoReady $Url $ReadyTimeoutSeconds $process
}

Write-Section "Demo launched"

$cacheBustedUrl = "$Url/?v=$([DateTimeOffset]::UtcNow.ToUnixTimeSeconds())"

Write-Host "PID saved to: $PidFile"
Write-Host "stdout: $OutLog"
Write-Host "stderr: $ErrLog"
Write-Host "URL: $cacheBustedUrl"
