. .\Variables.ps1

Write-Host ('Build') -f 'green'
& $msbuild_path $solution_path -verbosity:minimal  -nologo -t:build -property:Configuration=Debug