using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL.Models
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        // string because 001 would turn to 1 if int. sometimes want 001
        // make string if you dont do math with it or can start with 0.
        // zip, phone, ssn, ect...
        [DisplayName("Student Id")]
        public string? StudentId { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return LastName + ", " + FirstName; } }

        public List<Advisor> Advisors { get; set; } = new List<Advisor>();
    }
}
