using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;

namespace DDB.ProgDec.BL
{
    public static class ProgramManager
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

        public static Program LoadByID(int id)
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

        public static List<Program> Load()
        {
            try
            {
                List<Program> list = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from p in dc.tblPrograms
                     select new
                     {
                         p.Id,
                         p.Description,
                         p.DegreeTypeId
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
