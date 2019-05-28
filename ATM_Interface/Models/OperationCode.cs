using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Interface.Models
{
    public class OperationCode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}