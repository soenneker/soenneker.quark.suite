# StopDemo.ps1
# Stops the Demo service by reading from its PID file

$PidFile = Join-Path $PSScriptRoot ".Demo-pid.txt"

Write-Host "Stopping Demo service..."

function Stop-ServiceByPidFile($serviceName, $pidFile) {
    if (-not (Test-Path $pidFile)) { 
        Write-Host "No PID file found for $serviceName"
        return 
    }

    Write-Host "Stopping $serviceName..."
    Get-Content $pidFile | Where-Object { $_ -match '^\d+$' } | Select-Object -Unique | ForEach-Object {
        try {
            if ($IsWindows) {
                Start-Process -FilePath "taskkill.exe" -ArgumentList "/PID $_ /T /F" -NoNewWindow -Wait
            } else {
                $p = Get-Process -Id ([int]$_) -ErrorAction SilentlyContinue
                if ($p) { $p.CloseMainWindow() | Out-Null; Start-Sleep -Milliseconds 200 }
                Stop-Process -Id ([int]$_) -Force -ErrorAction SilentlyContinue
            }
        } catch {}
    }
    Remove-Item $pidFile -ErrorAction SilentlyContinue
}

# Stop Demo service
Stop-ServiceByPidFile "Demo" $PidFile

# Fallback: kill any lingering dotnet watch processes for Demo
try {
    Get-CimInstance Win32_Process -Filter "Name='dotnet.exe'" |
        Where-Object { $_.CommandLine -match 'watch\s+run' -and $_.CommandLine -match 'Soenneker.Quark.Suite\.Demo' } |
        ForEach-Object { Invoke-CimMethod -InputObject $_ -MethodName Terminate | Out-Null }
} catch {}

Write-Host "Demo service stopped."
