using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ATM_Interface.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        [Display(Name = "Enter card number:")]
        public string CardNumber { get; set; }

        [Display(Name = "Enter PIN code:")]
        public string PIN { get; set; }
        public decimal AvailableMoney { get; set; }
        public bool Status { get; set; } // Lock or unlock
    }
}