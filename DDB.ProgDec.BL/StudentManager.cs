using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;

namespace DDB.ProgDec.BL
{
    public static class StudentManager
    {
        public static int Insert()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }

        public static int Update() 
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }

        public static int Delete()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }

        public static Student LoadByID(int id)
        {
            try
            {
                return null;
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
