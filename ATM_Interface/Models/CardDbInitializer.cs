using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ATM_Interface.Models
{
    public class CardDbInitializer : DropCreateDatabaseIfModelChanges<CardContext>
    {
        protected override void Seed(CardContext db)
        {
            db.Cards.Add(new Card { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3000"), CardNumber = "1111111111111111", Status = true, AvailableMoney = 1000, PIN = "1111"});
            db.Cards.Add(new Card { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3001"), CardNumber = "2222222222222222", Status = true, AvailableMoney = 2000, PIN = "2222"});
            db.Cards.Add(new Card { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3002"), CardNumber = "3333333333333333", Status = true, AvailableMoney = 500, PIN = "1111"});

            db.OperationType.Add(new OperationType { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C4000"), Name = "Withdrawn" });
            db.OperationType.Add(new OperationType { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C4001"), Name = "Refill" });

            db.OperationCode.Add(new OperationCode { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C5000"), Name = "Balans", Code = 1 });
            db.OperationCode.Add(new OperationCode { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C5001"), Name = "WithdrawMoney", Code = 2 });
            db.OperationCode.Add(new OperationCode { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C5002"), Name = "PINCode", Code = 3 });
        }
    }
}
