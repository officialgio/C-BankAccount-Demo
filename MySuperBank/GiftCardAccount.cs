using System;

namespace MySuperBank
{
    public class GiftCardAccount : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;

        //The constructor provides a default value
        //for the monthlyDeposit value so callers can omit a 0 for no monthly deposit.
        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name,
            initialBalance) => _monthlyDeposit = monthlyDeposit;

        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}