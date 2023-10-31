
namespace DDB.ProgDec.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoginSuccessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "maple" }));
            Assert.IsTrue(UserManager.Login(new User { UserId = "DBartel", Password = "password" }));
        }
        public void Seed()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {

        }
        [TestMethod]
        public void LoginFailureNoUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "mapl" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "Bfootes", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "bfoote", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
