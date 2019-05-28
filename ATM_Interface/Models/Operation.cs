using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Interface.Models
{
    public class Operation
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public Guid OperationTypeId { get; set; }
        public decimal WithdrawnMoney { get; set; }        
        public DateTime Date { get; set; }
    }
}