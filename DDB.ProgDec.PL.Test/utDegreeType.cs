using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
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
            Assert.AreEqual(3, dc.tblDegreeTypes.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            // Make an Entity
            tblDegreeType entity = new tblDegreeType();
            entity.Description = "Masters";
            entity.Id = -99;

            //Add entity to database
            dc.tblDegreeTypes.Add(entity);
            // commit the changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * from tblDegreeType - Use the first one
            tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault();

            // Change property values
            entity.Description = "Masters";


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // select * from tblDegreeType where id = 4;
            tblDegreeType entity = dc.tblDegreeTypes.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblDegreeTypes.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblDegreeType entity = dc.tblDegreeTypes.Where(e => e.Id == 2).FirstOrDefault();

            Assert.AreEqual(entity.Id, 2);
        }
    }
}
