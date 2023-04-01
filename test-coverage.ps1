
if (Test-Path -Path .\coverage-*) {
  Remove-Item .\coverage-* -Recurse -Force
}

dotnet test --settings coverage.runsettings --results-directory .\coverage-xml --diag .\coverage-logs\log.txt
reportgenerator -reports:"./coverage-xml/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"html"
