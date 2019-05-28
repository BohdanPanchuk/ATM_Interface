using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Interface.Models
{
    public class OperationWithCard
    {
        public Guid Id { get; set; }
        public Guid OperationCodeId { get; set; }
        public DateTime Date { get; set; }
    }
}