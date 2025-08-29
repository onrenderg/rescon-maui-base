# PowerShell script to create Android keystore
# Run this script to generate signing key for AAB

$keystoreName = "ResillentConstruction.keystore"
$alias = "ResillentConstructionKey"

Write-Host "Creating Android keystore for AAB signing..." -ForegroundColor Green

# Check if Java/keytool is available
$javaHome = $env:JAVA_HOME
if (-not $javaHome) {
    # Try to find Java in common locations
    $possiblePaths = @(
        "${env:ProgramFiles}\Java\*\bin\keytool.exe",
        "${env:ProgramFiles(x86)}\Java\*\bin\keytool.exe",
        "${env:LOCALAPPDATA}\Microsoft\WindowsApps\keytool.exe"
    )
    
    foreach ($path in $possiblePaths) {
        $keytool = Get-ChildItem $path -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($keytool) {
            $keytoolPath = $keytool.FullName
            break
        }
    }
}
else {
    $keytoolPath = "$javaHome\bin\keytool.exe"
}

if ($keytoolPath -and (Test-Path $keytoolPath)) {
    Write-Host "Found keytool at: $keytoolPath" -ForegroundColor Yellow
    
    # Generate keystore
    & $keytoolPath -genkey -v -keystore $keystoreName -alias $alias -keyalg RSA -keysize 2048 -validity 10000
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Keystore created successfully: $keystoreName" -ForegroundColor Green
        Write-Host "Remember to keep this file and password safe!" -ForegroundColor Yellow
    }
}
else {
    Write-Host "Java keytool not found. Please install Java JDK or use Visual Studio method." -ForegroundColor Red
    Write-Host "Alternative: Use Visual Studio Archive feature or Android Studio." -ForegroundColor Yellow
}
