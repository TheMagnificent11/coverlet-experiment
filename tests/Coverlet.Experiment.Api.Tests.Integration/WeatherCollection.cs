using Xunit;

namespace Coverlet.Experiment.Api.Tests.Integration;

[CollectionDefinition(nameof(WeatherCollection))]
public sealed class WeatherCollection : ICollectionFixture<WeatherDatabaseFixture>
{
}
