﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Password { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
