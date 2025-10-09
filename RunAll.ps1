# RunAll.ps1
# Launches all services by calling individual service scripts, waits for readiness, then opens browsers.

[CmdletBinding()]
param([switch]$NoPreRestore)

$ErrorActionPreference = "Stop"

function Write-Section($msg) { Write-Host "`n=== $msg ===" -ForegroundColor Cyan }

# --- One-time prep ----------------------------------------------------------------
Write-Section "Stopping all existing services"
& "$PSScriptRoot\StopAll.ps1"

Write-Section "Shutting down lingering build servers"
dotnet build-server shutdown | Out-Null

# --- Launch -----------------------------------------------------------------------
Write-Section "Starting services"
& "$PSScriptRoot\RunDemo.ps1"

Write-Section "All services launched."
