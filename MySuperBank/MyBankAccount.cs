using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySuperBank
{
    public class BankAccount
    {
        public String Owner { get; }
        public String Number { get; }

        private readonly decimal _minimumBalance;


        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in _allTransactions)
                {
                    balance += item.amount;
                }

                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;

        private List<Transaction> _allTransactions = new List<Transaction>();

        public BankAccount(string owner, decimal initialBalance)
        {
            this.Owner = owner;
            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
        }

        // additional constructors

        //constructor that takes two parameters uses : this(name, initialBalance, 0) { }
        //as its implementation. The : this() expression calls the other constructor, the one with three parameters. 
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
        {
        }

        // construcotr with 3 param
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            
            Owner = name;
            _minimumBalance = minimumBalance;
            if (initialBalance > 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
            }
        }


        // 
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            Transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);
            if (overdraftTransaction != null)
                _allTransactions.Add(overdraftTransaction);
        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        public string getAccountHistory()
        {
            var report = new StringBuilder();
            //Header 
            report.AppendLine("Date\tAmount\tNote");

            foreach (var item in _allTransactions)
            {
                // Rows
                report.AppendLine($"{item.Date}\t{item.amount}\t{item.notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions()
        {
        }
    }
}