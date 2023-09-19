

namespace DDB.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
    {
        protected ProgDecEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize() 
        { 
            dc = new ProgDecEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, dc.tblPrograms.Count());
        }

        [TestMethod]
        public void InsertTest() 
        {

            // Make an Entity
            tblProgram entity = new tblProgram();
            entity.DegreeTypeId = 2;
            entity.Description = "Basket Weaving";
            entity.Id = -99;

            //Add entity to database
            dc.tblPrograms.Add(entity);
            // commit the changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * from tblProgram - Use the first one
            tblProgram entity = dc.tblPrograms.FirstOrDefault();

            // Change property values
            entity.Description = "New Description";
            entity.DegreeTypeId = 3;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // select * from tblProgram where id = 4;
            tblProgram entity = dc.tblPrograms.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblPrograms.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblProgram entity = dc.tblPrograms.Where(e => e.Id == 2).FirstOrDefault();

            Assert.AreEqual(entity.Id, 2);
        }
    }
}