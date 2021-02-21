using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListApp.Test.UnitTests.Services
{
    public abstract class BaseServiceTests
    {
        [TestInitialize]
        public virtual void BeforeEachTest()
        {
            CreateMocks();
            SetUpMocks();
        }

        protected virtual void CreateMocks()
        {
        }

        protected virtual void SetUpMocks()
        {

        }
    }
}
