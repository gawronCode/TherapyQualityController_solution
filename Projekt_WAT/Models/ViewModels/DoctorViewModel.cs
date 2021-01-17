﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models.ViewModels
{
    public class DoctorViewModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PESEL { get; set; }

        public string PWZ { get; set; }

        public bool IsConfirmed { get; set; }

    }
}
