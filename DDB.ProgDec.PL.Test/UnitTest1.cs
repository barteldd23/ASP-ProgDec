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
    }
}