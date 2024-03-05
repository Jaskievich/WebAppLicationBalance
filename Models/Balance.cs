namespace WebApplicationBalance.Models
{
    public class Balance
    {
        public string PersonalAccount { get; }

        public Decimal Amount { get; }

        public Balance(string PersonalAccount, Decimal Amount)
        {
            this.PersonalAccount = PersonalAccount;
            this.Amount = Amount;
        }

    }
}
