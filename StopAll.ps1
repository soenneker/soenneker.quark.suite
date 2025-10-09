# StopAll.ps1
# Stops all services by calling individual stop scripts

Write-Host "Stopping all services..."

# Call individual stop scripts
& "$PSScriptRoot\StopDemo.ps1"

Write-Host "Done."
