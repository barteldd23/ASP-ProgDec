﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL.Models
{
    internal class Declaration
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
