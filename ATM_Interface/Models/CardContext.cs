using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ATM_Interface.Models
{
    public class CardContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationType> OperationType { get; set; }
        public DbSet<OperationCode> OperationCode { get; set; }
        public DbSet<OperationWithCard> OperationsWithCard { get; set; }
    }
}
