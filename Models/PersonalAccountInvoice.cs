using System.ComponentModel.DataAnnotations;

namespace WebApplicationBalance.Models
{
    public class PersonalAccountInvoice
    {
        public int Id { get; set; }
        [Display(Name = "Выберите счет")]
        [Required(ErrorMessage = "Выберите счет")]
        public int PersonalAccountId { get; set; } 
      
        public string Account { get; set; }

        [Display(Name = "Введите сумму")]
        [Required(ErrorMessage = "Введите сумму")]
        public Decimal SumAccount { get; set; }
        [Display(Name = "Введите описание")]
        public string Description { get; set; }
        public PersonalAccountInvoice() { }
        public PersonalAccountInvoice(int _Id, int _PersonalAccountId, string _Account, Decimal _SumAccount, string _Description)
        {
            Id = _Id;
            PersonalAccountId = _PersonalAccountId;
            Account = _Account;
            SumAccount = _SumAccount;
            Description = _Description;
        }
    }
}
