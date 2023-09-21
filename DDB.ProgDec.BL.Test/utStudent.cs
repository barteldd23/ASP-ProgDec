namespace DDB.ProgDec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(5,StudentManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = StudentManager.Insert("Bale", "Organa", "555555555", ref id, true);
            Assert.AreEqual(1,results);
            Assert.AreEqual(6, id);
        }

        [TestMethod]
        public void InsertTest2()
        {
            {
                int id = 0;
                Student student = new Student
                {
                    FirstName = "test",
                    LastName = "test",
                    StudentId = "test"
                };
                int results = StudentManager.Insert(student, true);
                Assert.AreEqual(1, results);
            }
        }
    }
}