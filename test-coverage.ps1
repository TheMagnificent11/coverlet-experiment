if (Test-Path -Path .\coverage) {
  Remove-Item .\coverage -Recurse -Force
}

if (Test-Path -Path .\coverage-report) {
  Remove-Item .\coverage-report -Recurse -Force
}

dotnet test --settings coverage.runsettings --results-directory ./coverage --diag ./coverage-logs
reportgenerator -reports:"./coverage/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"html"
