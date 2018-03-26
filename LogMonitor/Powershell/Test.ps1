. .\Variables.ps1

Write-Host ('Test') -f 'green'
& $mstest_path /testmetadata:$unittest_dll_path