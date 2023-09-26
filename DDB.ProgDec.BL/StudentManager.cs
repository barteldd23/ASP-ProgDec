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
    public static class StudentManager
    {
        public static int Insert(string firstName, 
                                 string lastName, 
                                 string studentId,
                                 ref int id,
                                 bool rollback = false) // need this optional parameter for the testing. send True only from ut files.
                                                        // Because if false, then its the actual program and we want to keep the data in DB.
        {
            try
            {
                Student student = new Student 
                { 
                    FirstName = firstName,
                    LastName = lastName,
                    StudentId = studentId,
                };

                int results = Insert(student, rollback); // objects are always passed by reference, so 'student' can get changed in the next method.

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = student.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public static int Insert(Student student, bool rollback = false)
        {
            int results = 0;
            try
            {
                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    //for rollback on ut files
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblStudent entity = new tblStudent();
                    entity.Id = dc.tblStudents.Any() ? dc.tblStudents.Max(s=> s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                    entity.FirstName = student.FirstName;
                    entity.LastName = student.LastName;
                    entity.StudentId = student.StudentId;


                    // IMPORTANT - BACK FILL THE ID
                    student.Id = entity.Id; //do this because the first Insert is id by reference


                    //Add entity to database
                    dc.tblStudents.Add(entity);
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

        public static int Update(Student student, bool rollback=false) 
        {
            try
            {
                int results = 0;
                using(ProgDecEntities dc =new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblStudent entity = dc.tblStudents.FirstOrDefault(s => s.Id == student.Id);

                    if(entity != null)
                    {
                        entity.FirstName = student.FirstName;
                        entity.LastName = student.LastName;
                        entity.StudentId = student.StudentId;
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

        //dont need to send a full student in this case. id is sufficient enough.
        public static int Delete(int id, bool rollback=false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblStudent entity = dc.tblStudents.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblStudents.Remove(entity);
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

        public static Student LoadByID(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudent entity = dc.tblStudents.FirstOrDefault(student => student.Id == id);

                    if (entity != null)
                    {
                        return new Student
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            StudentId = entity.StudentId
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

        public static List<Student> Load()
        {
            try
            {
                List<Student> list = new List<Student>();

                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblStudents
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.StudentId
                     })
                     .ToList()
                     .ForEach(student => list.Add(new Student
                     {
                         Id = student.Id,
                         FirstName = student.FirstName,
                         LastName = student.LastName,
                         StudentId = student.StudentId

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
