namespace DDB.ProgDec.BL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, DegreeTypeManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = DegreeTypeManager.Insert("Test", ref id, true);
            Assert.AreEqual(1, results);
           // Assert.AreEqual(6, id);
        }

        [TestMethod]
        public void InsertTest2()
        {
            {
                int id = 0;
                DegreeType degreeType = new DegreeType
                {
                    Description = "test"
                };
                int results = DegreeTypeManager.Insert(degreeType, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            {
                DegreeType degreeType = DegreeTypeManager.LoadByID(3);
                degreeType.Description = "test update"; // need to physically change a value.
                                            // "updating" to the same values, does not change a row of data"
                                            // so it returns 0 still.

                int results = DegreeTypeManager.Update(degreeType, true);
                Assert.AreEqual(1, results);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            {

                int results = DegreeTypeManager.Delete(3, true);
                Assert.AreEqual(1, results);
            }
        }
    }
}