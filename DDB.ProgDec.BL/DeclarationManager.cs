using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDB.ProgDec.BL.Models;
using DDB.ProgDec.PL;

namespace DDB.ProgDec.BL
{
    public static class DeclarationManager
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

        public static Declaration LoadByID(int id)
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

        public static List<Declaration> Load()
        {
            try
            {
                List<Declaration> list = new List<Declaration>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from d in dc.tblDeclarations
                     select new
                     {
                         d.Id,
                         d.ProgramId,
                         d.StudentId,
                         d.ChangeDate
                     })
                     .ToList()
                     .ForEach(declaration => list.Add(new Declaration
                     {
                         Id = declaration.Id,
                         ProgramId = declaration.ProgramId,
                         StudentId = declaration.StudentId,
                         ChangeDate = declaration.ChangeDate
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
