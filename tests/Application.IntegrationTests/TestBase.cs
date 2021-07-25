using NUnit.Framework;
using System.Threading.Tasks;

namespace HPC.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }

        [OneTimeTearDown]
        public async Task TestOneTimeTearDown()
        {
            await ResetState();
        }
    }
}
