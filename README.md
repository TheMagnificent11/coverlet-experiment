# Coverlet Experiment

## Purpose

I am using `Coverlet` on an open source project ([Lewee](https://github.com/TheMagnificent11/lewee)) and getting 0% coverage for integration tests run using
`Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<T>`.

I've created this repository as a more minimal replication of the issue.

My hypothesis is that problem may occur when you use an implemenation of `Xunit.ICollectionFixture<T>` to "reset" a database before each test.

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
