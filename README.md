# Coverlet Experiment

## Purpose

I am using `Coverlet` on an open source project ([Lewee](https://github.com/TheMagnificent11/lewee)) and getting 0% coverage for integration tests run using
`Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`.

I've created this repository as a more minimal version of the integration tests; I've only added `Mediatr` as a package reference outside of the packages
automatically added when create a Web API project using `dotnet new webapi`.

## Running the experiment

Running this experiment requires PowerShell.

### Install Report Generator

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### Run Tests & Build Report

```bash
.\test-coverage.ps1
```

### View Report

```bash
.\coverage-report\index.html
```
