using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;

namespace DDB.ProgDec.BL
{
    public static class DegreeTypeManager
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

        public static DegreeType LoadByID(int id)
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

        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> list = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from dt in dc.tblDegreeTypes
                     select new
                     {
                         dt.Id,
                         dt.Description           
                     })
                     .ToList()
                     .ForEach(degreeType => list.Add(new DegreeType
                     {
                         Id = degreeType.Id,
                         Description = degreeType.Description
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
