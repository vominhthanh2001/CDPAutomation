﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class OptionNavigateModel
    {
        public int Timeout { get; set; } = 60;
        public bool IgnoreCache { get; set; }
        public bool WaitUntilPageLoad { get; set; } = true;
    }
}
