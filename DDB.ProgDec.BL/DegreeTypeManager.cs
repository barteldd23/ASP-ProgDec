using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DDB.ProgDec.BL
{
    public static class DegreeTypeManager
    {
        public static int Insert(string description,
                                 ref int id,
                                 bool rollback = false) // need this optional parameter for the testing. send True only from ut files.
                                                        // Because if false, then its the actual program and we want to keep the data in DB.
        {
            try
            {
                DegreeType degreeType = new DegreeType
                {
                    Description = description
                };

                int results = Insert(degreeType, rollback); // objects are always passed by reference, so 'degreeType' can get changed in the next method.

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = degreeType.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(DegreeType degreeType, bool rollback = false)
        {
            int results = 0;
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDegreeType entity = new tblDegreeType();
                    entity.Id = dc.tblDegreeTypes.Any() ? dc.tblDegreeTypes.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.Description = degreeType.Description;


                    foreach(Program program in degreeType.Programs)
                    {
                        // Set the orderId on tblOrderItem for chkpt 4
                        results += ProgramManager.Insert(program, rollback);
                    }

                    // IMPORTANT - BACK FILL THE ID
                    degreeType.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblDegreeTypes.Add(entity);
                    // commit the changes
                    results += dc.SaveChanges();

                    //only for ut files
                    if (rollback) transaction.Rollback();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }

        public static int Update(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == degreeType.Id);

                    if (entity != null)
                    {
                        entity.Description = degreeType.Description;
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //dont need to send a full degreeType in this case. id is sufficient enough.
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblDegreeTypes.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DegreeType LoadByID(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(degreeType => degreeType.Id == id);

                    if (entity != null)
                    {
                        return new DegreeType
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            Programs = ProgramManager.Load(entity.Id)
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> list = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblDegreeTypes
                     select new
                     {
                         s.Id,
                         s.Description
                     })
                     .ToList()
                     .ForEach(degreeType => list.Add(new DegreeType
                     {
                         Id = degreeType.Id,
                         Description = degreeType.Description,
                         Programs = ProgramManager.Load(degreeType.Id)

                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
