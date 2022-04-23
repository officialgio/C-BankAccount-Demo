using System;
namespace MySuperBank
{
    public class Transaction
    {
        public decimal amount { get; set; }
        public DateTime Date { get; set; }
        public string notes { get; set; }

        public Transaction(decimal amount, DateTime date, string notes)
        {
            this.amount = amount;
            this.Date = date;
            var s = notes;
            this.notes = s;
        }

        public double Amount { get; internal set; }
        
        

    }
}
