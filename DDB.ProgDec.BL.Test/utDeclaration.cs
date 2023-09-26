namespace DDB.ProgDec.BL.Test
{
    [TestClass]
    public class utDeclaration
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(4, DeclarationManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = DeclarationManager.Insert(123456789, 4, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertTest2()
        {
            {
                int id = 0;
                Declaration declaration = new Declaration
                {
                    StudentId = 123456789,
                    ProgramId = 4
                };
                int results = DeclarationManager.Insert(declaration, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            {
                Declaration declaration = DeclarationManager.LoadByID(3);
                declaration.StudentId = 999999999; // need to physically change a value.
                                            // "updating" to the same values, does not change a row of data"
                                            // so it returns 0 still.

                int results = DeclarationManager.Update(declaration, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            {

                int results = DeclarationManager.Delete(3, true);
                Assert.AreEqual(1, results);
            }
        }
    }
}