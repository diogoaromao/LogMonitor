# Paths
$projects_path = Split-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) -Parent
Write-Host ('Projects Path') -f 'green' $projects_path

$logmonitor_path = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
Write-Host ('Log Monitor') -f 'green' $logmonitor_path

$msbuild_path = 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MsBuild.exe'
Write-Host ('Build Path') -f 'green' $msbuild_path

$mstest_path = 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\MSTest.exe'
Write-Host ('Test Path') -f 'green' $mstest_path

$solution_path = ($logmonitor_path +'\LogMonitor.sln')
Write-Host ('Solution Path') -f 'green' $solution_path

$unittest_dll_path = ($logmonitor_path +'\LogMonitor.UnitTesting\bin\Debug\LogMonitor.UnitTesting.dll')
Write-Host ('Testing DLL') -f 'green' $unittest_dll_path