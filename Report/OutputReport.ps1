 
$openCoverFilters = "+[MediaStorage.Service]*"  
$vstestPath = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
$openCoverPath = "$PSScriptRoot\..\\Packages\OpenCover.4.6.519\tools\OpenCover.Console.exe"
$reportGeneratorPath = "$PSScriptRoot\..\\Packages\ReportGenerator.4.2.0\tools\net47\ReportGenerator.exe"
$trxMergerPath = "$PSScriptRoot\..\\packages\TRX-Merger.1.0.0\lib\TRX_Merger.exe"
$testFilesString="$PSScriptRoot\..\\MediaStorage.Service.Test\bin\Debug\MediaStorage.Service.Test.dll"
$browserPath="C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"



Write-Host $MyInvocation.MyCommand ": Removing old testresults"
Remove-Item -Path $PSScriptRoot\TestResults -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path $PSScriptRoot\CoverageReport -Recurse -Force -ErrorAction SilentlyContinue 
New-Item -ItemType directory -Path $PSScriptRoot\CoverageReport

Start-Process "$openCoverPath" -wait -NoNewWindow `
-ArgumentList "-register:user -filter:""$OpenCoverFilters"" -target:""$vstestPath"" `
-targetargs:""$testFilesString /logger:trx"" -output:""$PSScriptRoot\CoverageReport\OpenCoverReport.xml"" `
-mergebyhash" -WorkingDirectory $PSScriptRoot


Start-Process "$reportGeneratorPath" `
-Wait -NoNewWindow -ArgumentList "-reports:""$PSScriptRoot\CoverageReport\OpenCoverReport.xml"" `
-targetdir:""$PSScriptRoot\CoverageReport"""


Start-Process "$trxMergerPath" `
-Wait -NoNewWindow -ArgumentList "/trx:""$PSScriptRoot\TestResults"" /report:""$PSScriptRoot\CoverageReport\TestCaseOutputReport.html"""

Remove-Item -Path $PSScriptRoot\TestResults -Recurse -Force -ErrorAction SilentlyContinue

Start-Process -FilePath "$browserPath" -ArgumentList "$PSScriptRoot\CoverageReport\TestCaseOutputReport.html"
Start-Process -FilePath "$browserPath" -ArgumentList "$PSScriptRoot\CoverageReport\index.htm"