﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Interface.Models
{
    public class OperationResult
    {
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal WithdrawnMoney { get; set; }
        public decimal AvailableMoney { get; set; }
    }
}