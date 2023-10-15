param(
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]$appName
)

Get-PSBreakpoint | Remove-PSBreakpoint

Get-ChildItem -Recurse | rename-item -NewName {$_.name -replace "VocabularyApp",$appName} -ErrorAction SilentlyContinue
Get-ChildItem -Recurse -File | % {((Get-Content $_.FullName) -replace "VocabularyApp",$appName) | out-file -Encoding UTF8 $_.FullName}
