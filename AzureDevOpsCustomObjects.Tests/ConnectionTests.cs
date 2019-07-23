using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AzureDevOpsCustomObjects.Tests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public async Task TestConnection()
        {
            //Arrange
            string organization = "samsmithnz";
            string project = "AzureDevopsScheduler";
            string personalAccessToken = "uqr2t7exxqpvdvy5srufzk54qftru3oxq53yomsfmhdp64mrcdla";
            Connection conn = new Connection();

            //Act
            bool result = await conn.CreateConnection(organization, project, personalAccessToken);

            //Asset
            Assert.IsTrue(result);
        }
    }
}
