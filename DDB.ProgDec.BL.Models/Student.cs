using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // string because 001 would turn to 1 if int. sometimes want 001
        // make string if you dont do math with it or can start with 0.
        // zip, phone, ssn, ect...
        public string? StudentId { get; set; }
    }
}
