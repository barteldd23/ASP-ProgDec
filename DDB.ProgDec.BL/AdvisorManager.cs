using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DDB.ProgDec.BL
{
    public static class AdvisorManager
    {
        public static int Insert(string name,
                                 ref int id,
                                 bool rollback = false) // need this optional parameter for the testing. send True only from ut files.
                                                        // Because if false, then its the actual program and we want to keep the data in DB.
        {
            try
            {
                Advisor advisor = new Advisor
                {
                    Name = name
                };

                int results = Insert(advisor, rollback); // objects are always passed by reference, so 'advisor' can get changed in the next method.

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = advisor.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(Advisor advisor, bool rollback = false)
        {
            int results = 0;
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAdvisor entity = new tblAdvisor();
                    entity.Id = dc.tblAdvisors.Any() ? dc.tblAdvisors.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.Name = advisor.Name;

                    // IMPORTANT - BACK FILL THE ID
                    advisor.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblAdvisors.Add(entity);
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

        public static int Update(Advisor advisor, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == advisor.Id);

                    if (entity != null)
                    {
                        entity.Name = advisor.Name;
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

        //dont need to send a full advisor in this case. id is sufficient enough.
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
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblAdvisors.Remove(entity);
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

        public static Advisor LoadByID(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(advisor => advisor.Id == id);

                    if (entity != null)
                    {
                        return new Advisor
                        {
                            Id = entity.Id,
                            Name = entity.Name
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

        public static List<Advisor> Load()
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from a in dc.tblAdvisors
                     select new
                     {
                         a.Id,
                         a.Name
                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name

                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Advisor> Load(int studentId)
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from a in dc.tblAdvisors
                     join sa in dc.tblStudentAdvisors on a.Id equals sa.AdvisorId
                     where sa.StudentId == studentId
                     select new
                     {
                         a.Id,
                         a.Name
                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name

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
