﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models
{
    public class QuestionnaireViewModel
    {
        public string Name { get; set; }
        public List<FieldViewModel> Fields { get; set; }
    }
}