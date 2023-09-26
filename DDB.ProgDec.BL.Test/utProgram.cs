namespace DDB.ProgDec.BL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, ProgramManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = ProgramManager.Insert("Test", 2, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertTest2()
        {
            {
                int id = 0;
                Program program = new Program
                {
                    Description = "test",
                    DegreeTypeId = 2
                };
                int results = ProgramManager.Insert(program, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            {
                Program program = ProgramManager.LoadByID(3);
                program.Description = "test update"; // need to physically change a value.
                                            // "updating" to the same values, does not change a row of data"
                                            // so it returns 0 still.

                int results = ProgramManager.Update(program, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            {

                int results = ProgramManager.Delete(3, true);
                Assert.AreEqual(1, results);
            }
        }
    }
}