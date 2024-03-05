using System.ComponentModel.DataAnnotations;

namespace WebApplicationBalance.Models
{
    public class PersonalAccountPayment
    {
        public int Id { get; set; }
        [Display(Name = "Выберите лицевой счет")]
        [Required(ErrorMessage = "Выберите лицевой счет")]
        public int PersonalAccountId { get; set; }

        public string Account {  get; set; }    
        [Display(Name = "Введите сумму")]
        [Required(ErrorMessage = "Введите сумму")]
        public Decimal SumPayment { get; set; }
        [Display(Name = "Введите назначение платежа")]
        public string PurposePayment { get; set; }

        public PersonalAccountPayment() { }
        public PersonalAccountPayment(int id, int PersonalAccountId, string _Account, Decimal SumPayment, string Description)
        {
            this.Id = id;
            this.PersonalAccountId = PersonalAccountId;
            this.Account = _Account;
            this.SumPayment = SumPayment;
            this.PurposePayment = Description;
        }
    }
}
