using Tests.Core.Fixtures.Contexts.Linq2Db;
using Xunit;

namespace Tests.Core.Fixtures.Collections.Linq2Db
{
    [CollectionDefinition("Linq2DbDatabase")]
    public class Linq2DbDatabaseCollectionFixture : ICollectionFixture<Linq2DbDatabaseFixture>
    {
    }
}
