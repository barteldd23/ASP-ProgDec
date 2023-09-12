namespace DDB.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            ProgDecEntities dc = new ProgDecEntities();
            Assert.AreEqual(16, dc.tblPrograms.Count());
        }

        [TestMethod]
        public void InsertTest() 
        {
            var dc = new ProgDecEntities();

            // Make an Entity
            tblProgram entity = new tblProgram();
            entity.DegreeTypeId = 2;
            entity.Description = "Basket Weaving";
            entity.Id = -99;

            //Add entity to database
            dc.tblPrograms.Add(entity);
            // commit the changes
            int result = dc.SaveChanges();
        }
    }
}