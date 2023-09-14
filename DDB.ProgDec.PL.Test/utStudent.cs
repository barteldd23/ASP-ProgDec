using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.PL.Test
{
    [TestClass]
    public class utStudent
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
            Assert.AreEqual(5, dc.tblStudents.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            // Make an Entity
            tblStudent entity = new tblStudent();
            entity.FirstName = "Dean";
            entity.LastName = "Weaving";
            entity.StudentId = "234567";
            entity.Id = -99;

            //Add entity to database
            dc.tblStudents.Add(entity);
            // commit the changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * from tblStudent - Use the first one
            tblStudent entity = dc.tblStudents.FirstOrDefault();

            // Change property values
            entity.FirstName = "Dean";
            entity.LastName = "Weaving";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // select * from tblStudent where id = 4;
            tblStudent entity = dc.tblStudents.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblStudents.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
