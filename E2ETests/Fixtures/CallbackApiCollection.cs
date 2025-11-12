using Xunit;

namespace E2ETests.Fixtures
{
    [CollectionDefinition("Callback API collection", DisableParallelization = true)]
    public class CallbackApiCollection
        : ICollectionFixture<CallbackApiApplicationFactory<CallbackAPI.Program>>,
          ICollectionFixture<E2ETestFixture>
    { }
}