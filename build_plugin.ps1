param(
    [string]$Version = $(Read-Host "Enter version (e.g. 4.0.8)")
)

if ([string]::IsNullOrWhiteSpace($Version)) { throw "Version cannot be empty." }
$ErrorActionPreference = 'Stop'

# --- Paths ---
$RootDir = Get-Location
$OutDir  = Join-Path $RootDir "out"
$Stage   = Join-Path $OutDir "staging_$Version"
$PkgBase = "MSFS2024_$Version"
$ZipPath   = Join-Path $OutDir "$PkgBase.zip"
$LplugPath = Join-Path $OutDir "$PkgBase.lplug4"

# Your original build outputs/inputs (do not change)
$RootBin = Join-Path $RootDir "bin\Release"
$MetadataDirBuilt = Join-Path $RootBin "metadata"
$PluginDll        = Join-Path $RootBin "MsfsPlugin.dll"

$WrapDir = Join-Path $RootDir "SimConnectWrapper\bin\Release\net8.0-windows10.0.19041.0"
$WrapDll = Join-Path $WrapDir "SimConnectWrapper.dll"
$SimcDll = Join-Path $WrapDir "Microsoft.FlightSimulator.SimConnect.dll"

$ExtraSimconnect = Join-Path $RootDir "MsfsPlugin\SimConnect.dll"

# Locate SOURCE LoupedeckPackage.yaml (edit BEFORE build)
$SrcYamlPath = Join-Path $RootDir "MsfsPlugin\MsfsPlugin\metadata\LoupedeckPackage.yaml"
if (-not (Test-Path $SrcYamlPath)) {
    $candidate = Get-ChildItem -Path (Join-Path $RootDir "MsfsPlugin\MsfsPlugin") -Recurse -Filter "LoupedeckPackage.yaml" -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($candidate) { $SrcYamlPath = $candidate.FullName }
}
if (-not (Test-Path $SrcYamlPath)) { throw "Could not find source metadata LoupedeckPackage.yaml" }

# --- Update version in source YAML (pre-restore/build) ---
Write-Host "=== Updating package version in source metadata ==="
$yaml = Get-Content -Raw -LiteralPath $SrcYamlPath
$updated = $yaml -replace '(?m)^(?<k>\s*version\s*:\s*).+$', "`${k}$Version"
if ($updated -ne $yaml) {
    Set-Content -LiteralPath $SrcYamlPath -Value $updated -Encoding UTF8
    Write-Host "Set version -> $Version in $SrcYamlPath"
} else {
    Write-Host "[WARN] No 'version:' line matched in $SrcYamlPath" -ForegroundColor Yellow
}

# --- Restore & Build ---
if (Test-Path $Stage) { Remove-Item -Recurse -Force $Stage }
New-Item -ItemType Directory -Path (Join-Path $Stage "metadata") | Out-Null
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir | Out-Null }

Write-Host "=== Restoring packages ==="
dotnet restore (Join-Path $RootDir "MsfsPlugin\MsfsPlugin.sln")
if ($LASTEXITCODE -ne 0) { throw "Restore failed." }

Write-Host "=== Building MsfsPlugin (Release) ==="
dotnet build (Join-Path $RootDir "MsfsPlugin\MsfsPlugin.sln") -c Release
if ($LASTEXITCODE -ne 0) { throw "Build failed." }

# --- Verify files we stage ---
$missing = @()
if (-not (Test-Path $PluginDll))        { $missing += $PluginDll }
if (-not (Test-Path $MetadataDirBuilt)) { $missing += $MetadataDirBuilt }
if (-not (Test-Path $ExtraSimconnect))  { $missing += $ExtraSimconnect }
if ($missing.Count -gt 0) {
    Write-Host "[ERROR] Missing required files:" -ForegroundColor Red
    $missing | ForEach-Object { Write-Host "  $_" -ForegroundColor Red }
    throw "Aborting due to missing files."
}

# --- Stage files ---
Write-Host "=== Staging files ==="
Copy-Item "$MetadataDirBuilt\*" (Join-Path $Stage "metadata") -Recurse -Force
Copy-Item $PluginDll        $Stage -Force
if (Test-Path $WrapDll) { Copy-Item $WrapDll $Stage -Force }
if (Test-Path $SimcDll) { Copy-Item $SimcDll $Stage -Force }
Copy-Item $ExtraSimconnect  $Stage -Force

# --- Create package (zip -> .lplug4) ---
Write-Host "=== Creating package ==="
if (Test-Path $ZipPath)   { Remove-Item $ZipPath -Force }
if (Test-Path $LplugPath) { Remove-Item $LplugPath -Force }

Compress-Archive -Path (Join-Path $Stage '*') -DestinationPath $ZipPath -Force -CompressionLevel Optimal
Rename-Item -Path $ZipPath -NewName (Split-Path $LplugPath -Leaf)

Write-Host "=== Done ==="
Write-Host "Package created: $LplugPath"
Invoke-Item $OutDir
