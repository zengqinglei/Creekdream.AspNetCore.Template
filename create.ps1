$packFolder = (Get-Item -Path "./" -Verbose).FullName
Write-Host ""
Write-Host 'Current working directory : ' $packFolder

Write-Host ""
$companyName = Read-Host 'Please enter the company name '
$projectName = Read-Host 'Please enter the project name '
Write-Host ""

$companNamePlaceholder = "CompanyName"
$projectNamePlaceholder = "ProjectName"

$excludedDirectory = ".git|.vs|obj|bin|tools"
$excludedFile ='create.ps1','*.gitignore','*.gitattributes'
$sourceFiles = Get-ChildItem -Path $packFolder -Exclude $excludedFile -Recurse | Sort-Object -Property Length -Descending | Where-Object{ ($_.FullName -notmatch $excludedDirectory )} 

foreach ($sourceFile in $sourceFiles)
{
	$targetFileName = $sourceFile.Name.Replace($companNamePlaceholder,$companyName).Replace($projectNamePlaceholder,$projectName)
    $targetFilePath = ""
	if($sourceFile -is [System.IO.FileInfo])
	{
        if($origin.Length -ge 0)
        {
            $sourceContent = Get-Content -Path $sourceFile.FullName -Encoding UTF8 -Raw
		    $targetContent = $sourceContent.Replace($companNamePlaceholder,$companyName).Replace($projectNamePlaceholder,$projectName)
		    Set-Content $sourceFile.FullName -Value $targetContent -Encoding UTF8
        }
		$targetFilePath = Join-Path $sourceFile.DirectoryName $targetFileName
	}
    else
    {
        $targetFilePath = Join-Path $sourceFile.Parent.FullName $targetFileName
    }
	if($targetFileName -ne $sourceFile.Name)
	{
        if(Test-Path $targetFilePath)
        {
            Remove-Item $targetFilePath -Recurse -Force
        }
		Rename-Item -Path $sourceFile.FullName -NewName $targetFileName
		Write-Host 'Replaced file : ' $targetFilePath
		Write-Host ""
	}
}
pause