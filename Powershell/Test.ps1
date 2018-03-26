. .\Variables.ps1

Write-Host ('Test') -f 'green'
& $mstest_path /testcontainer:$unittest_dll_path