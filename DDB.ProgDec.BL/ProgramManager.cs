using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.ContentModel;

namespace DDB.ProgDec.BL
{
    public static class ProgramManager
    {
        public static int Insert(string description,
                                 int degreeType, 
                                 ref int id,
                                 bool rollback = false) // need this optional parameter for the testing. send True only from ut files.
                                                        // Because if false, then its the actual program and we want to keep the data in DB.
        {
            try
            {
                Program program = new Program
                {
                    Description = description,
                    DegreeTypeId = degreeType
                };

                int results = Insert(program, rollback); // objects are always passed by reference, so 'program' can get changed in the next method.

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = program.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(Program program, bool rollback = false)
        {
            int results = 0;
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblProgram entity = new tblProgram();
                    entity.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.Description = program.Description;
                    entity.DegreeTypeId = program.DegreeTypeId;


                    // IMPORTANT - BACK FILL THE ID
                    program.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblPrograms.Add(entity);
                    // commit the changes
                    results = dc.SaveChanges();

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

        public static int Update(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == program.Id);

                    if (entity != null)
                    {
                        entity.Description = program.Description;
                        entity.DegreeTypeId = program.DegreeTypeId;
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

        //dont need to send a full program in this case. id is sufficient enough.
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
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblPrograms.Remove(entity);
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

        public static Program LoadByID(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(program => program.Id == id);

                    if (entity != null)
                    {
                        return new Program
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            DegreeTypeId = entity.DegreeTypeId
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

        public static List<Program> Load()
        {
            try
            {
                List<Program> list = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblPrograms
                     select new
                     {
                         s.Id,
                         s.Description,
                         s.DegreeTypeId
                     })
                     .ToList()
                     .ForEach(program => list.Add(new Program
                     {
                         Id = program.Id,
                         Description = program.Description,
                         DegreeTypeId = program.DegreeTypeId

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
