using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplicationBalance.Controllers;
namespace WebApplicationBalance.Models
{

    public class PersonalAccount
    {
      
        public int Id { get; set; }

        [Display(Name = "Лицевой счет")]
        [Required(ErrorMessage = "Лицевой счет")]
        public string Account { get; set; }
        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "ФИО")]
        public string? FullName { get; set; }

        public PersonalAccount() { }

        public PersonalAccount(int _Id, string _Account, string? _FullName) 
        {
            Id = _Id;

            Account = _Account;

            FullName = _FullName;
        }

        public PersonalAccount(string account)
        {
            Account = account;
        }
    }
}
