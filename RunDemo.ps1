# RunPortal.ps1
# Launches the Portal service with PID management

[CmdletBinding()]
param([switch]$NoPreRestore)

$ErrorActionPreference = "Stop"

# --- Paths / files ----------------------------------------------------------------
$PidFile = Join-Path $PSScriptRoot ".Demo-pid.txt"

function Write-Section($msg) { Write-Host "`n=== $msg ===" -ForegroundColor Cyan }

function Ensure-File($path) {
    if (-not (Test-Path $path)) {
        New-Item -Path $path -ItemType File -Force | Out-Null
    }
}

function Stop-ExistingSessions {
    if (-not (Test-Path $PidFile)) { return }

    Write-Section "Stopping previous session"
    Get-Content $PidFile | Where-Object { $_ -match '^\d+$' } | Select-Object -Unique | ForEach-Object {
        try {
            if ($IsWindows) {
                Start-Process -FilePath "taskkill.exe" -ArgumentList "/PID $_ /T /F" -NoNewWindow -Wait
            } else {
                $p = Get-Process -Id ([int]$_) -ErrorAction SilentlyContinue
                if ($p) { $p.CloseMainWindow() | Out-Null; Start-Sleep -Milliseconds 300 }
                Stop-Process -Id ([int]$_) -Force -ErrorAction SilentlyContinue
            }
        } catch {}
    }
    Start-Sleep -Milliseconds 500
    Remove-Item $PidFile -ErrorAction SilentlyContinue
}

function Start-Watch($projectPath, $title, $config, $extraArgs) {
    Ensure-File $PidFile
    $env:DOTNET_MSBUILDARGS = "/m:1"
    $env:ASPNETCORE_ENVIRONMENT = "Development"

    $args  = @("watch","run","-c",$config,"--no-restore","--verbose","--non-interactive") + $extraArgs
    $inner = "& { `$host.ui.RawUI.WindowTitle = '$title'; Write-Host '[$title] starting ($config)...'; dotnet $($args -join ' ') }"

    $proc = Start-Process -FilePath "pwsh.exe" `
                          -ArgumentList "-NoExit","-Command",$inner `
                          -WorkingDirectory $projectPath `
                          -WindowStyle Normal `
                          -PassThru
    Add-Content -Path $PidFile -Value $proc.Id
}

# --- One-time prep ----------------------------------------------------------------
Stop-ExistingSessions

if (-not $NoPreRestore) {
    Write-Section "Pre-restore (faster Hot Reload)"
    dotnet restore -c DebugDemo -r browser-wasm "test\Soenneker.Quark.Suite.Demo\Soenneker.Quark.Suite.Demo.csproj" | Out-Null
} else {
    Write-Host "(Skipping pre-restore)"
}

$env:DOTNET_USE_POLLING_FILE_WATCHER      = "1"
$env:DOTNET_WATCH_RESTART_ON_RUDE_EDIT    = "1"

# --- Launch -----------------------------------------------------------------------
Write-Section "Starting Demo watch"
Start-Watch "test\Soenneker.Quark.Suite.Demo" "Soenneker.Quark.Suite.Demo" "DebugDemo" @()

Write-Section "Demo launched. PID saved to $PidFile"