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
    public static class DeclarationManager
    {
        public static int Insert(int programId,
                                 int studentId,
                                 ref int id,
                                 bool rollback = false) // need this optional parameter for the testing. send True only from ut files.
                                                        // Because if false, then its the actual declaration and we want to keep the data in DB.
        {
            try
            {
                Declaration declaration = new Declaration
                {
                    ProgramId = programId,
                    StudentId = studentId,
                    ChangeDate = DateTime.Now
                };

                int results = Insert(declaration, rollback); // objects are always passed by reference, so 'declaration' can get changed in the next method.

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = declaration.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(Declaration declaration, bool rollback = false)
        {
            int results = 0;
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDeclaration entity = new tblDeclaration();
                    entity.Id = dc.tblDeclarations.Any() ? dc.tblDeclarations.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.ProgramId = declaration.ProgramId;
                    entity.StudentId = declaration.StudentId;
                    entity.ChangeDate = DateTime.Now;


                    // IMPORTANT - BACK FILL THE ID
                    declaration.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblDeclarations.Add(entity);
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

        public static int Update(Declaration declaration, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(s => s.Id == declaration.Id);

                    if (entity != null)
                    {
                        entity.ProgramId = declaration.ProgramId;
                        entity.StudentId = declaration.StudentId;
                        entity.ChangeDate = DateTime.Now;
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

        //dont need to send a full declaration in this case. id is sufficient enough.
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
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblDeclarations.Remove(entity);
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

        public static Declaration LoadByID(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(declaration => declaration.Id == id);

                    if (entity != null)
                    {
                        return new Declaration
                        {
                            Id = entity.Id,
                            ProgramId = entity.ProgramId,
                            StudentId = entity.StudentId,
                            ChangeDate = entity.ChangeDate
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

        public static List<Declaration> Load(int? programId=null)
        {
            try
            {
                List<Declaration> list = new List<Declaration>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from d in dc.tblDeclarations
                     join s in dc.tblStudents on d.StudentId equals s.Id
                     join p in dc.tblPrograms on d.ProgramId equals p.Id
                     join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                     where d.ProgramId == programId || programId == null
                     select new
                     {
                         d.Id,
                         StudentName = s.FirstName + " " + s.LastName,
                         ProgramName = p.Description,
                         DegreeTypeName = dt.Description,
                         d.ProgramId,
                         d.ChangeDate,
                         d.StudentId
                     })
                     .ToList()
                     .ForEach(declaration => list.Add(new Declaration
                     {
                         Id = declaration.Id,
                         ProgramId = declaration.ProgramId,
                         StudentId = declaration.StudentId,
                         ChangeDate = declaration.ChangeDate,
                         StudentName = declaration.StudentName,
                         ProgramName = declaration.ProgramName,
                         DegreeTypeName =declaration.DegreeTypeName

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
