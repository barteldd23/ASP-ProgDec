using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.PL.Test
{
    [TestClass]
    public class utDeclaration
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
            Assert.AreEqual(4, dc.tblDeclarations.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            // Make an Entity
            tblDeclaration entity = new tblDeclaration();
            entity.StudentId = 1655466;
            entity.ProgramId = 4;
            entity.ChangeDate = DateTime.Now;
            entity.Id = -99;

            //Add entity to database
            dc.tblDeclarations.Add(entity);
            // commit the changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * from tblDeclaration - Use the first one
            tblDeclaration entity = dc.tblDeclarations.FirstOrDefault();

            // Change property values
            entity.StudentId = 5;
            entity.ProgramId = 1;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // select * from tblDeclaration where id = 4;
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblDeclarations.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 2).FirstOrDefault();
    
            Assert.AreEqual(entity.Id, 2);
        }

    }
}
