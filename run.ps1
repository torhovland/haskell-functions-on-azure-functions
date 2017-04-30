$in = Get-Content $triggerInput
Write-Output "PowerShell script processed queue message '$in'"
$result = D:\home\site\wwwroot\FizzBuzzHaskell\Server\.cabal-sandbox\bin\FizzBuzzServer $in
Write-Output "Haskell calculated '$result'"
Out-File -encoding Utf8 -FilePath $output -inputObject $result