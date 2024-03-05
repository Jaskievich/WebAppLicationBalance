using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Principal;
using WebApplicationBalance.Models;

namespace WebApplicationBalance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private MyDataBase dataBase = new MyDataBase("Server=.\\SQLEXPRESS;Database=Balance;Trusted_Connection=True;") ;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<PersonalAccountInvoice> accountInvoiceCollection = new List<PersonalAccountInvoice>();
            if (dataBase.Open())
            {
                dataBase.GetTable(accountInvoiceCollection);
                dataBase.Close();
            }
            return View(accountInvoiceCollection);
        }

        public IActionResult Payment()
        {
            List<PersonalAccountPayment> paymentCollection = new List<PersonalAccountPayment>();
            if (dataBase.Open())
            {
                dataBase.GetTable(paymentCollection);
                dataBase.Close();
            }
            return View(paymentCollection);
        }

        public IActionResult Balance()
        {
            List<Balance> balanceCollection = new List<Balance>();
            if (dataBase.Open())
            {
                dataBase.GetTable(balanceCollection);
                dataBase.Close();
            }
            return View(balanceCollection);
        }

        [HttpGet]
        public IActionResult CreateAccountInvoice()
        {
            List<PersonalAccount> accountCollection = new List<PersonalAccount>();
            if (dataBase.Open())
            {
                dataBase.GetTable(accountCollection);
                dataBase.Close();
            }
            ViewBag.accounts = accountCollection;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccountInvoice(PersonalAccountInvoice personalAccountInvoice)
        {
            List<PersonalAccount> accountCollection = new List<PersonalAccount>();
            List<PersonalAccountInvoice> accountInvoiceCollection = new List<PersonalAccountInvoice>();
            if (dataBase.Open())
            {
                dataBase.AddPersonalAccountInvoice(personalAccountInvoice);
                dataBase.GetTable(accountCollection);
                dataBase.GetTable(accountInvoiceCollection);
                dataBase.Close();
            }
            ViewBag.accounts = accountCollection;
            return View("Index", accountInvoiceCollection);
        }

        [HttpGet]
        public IActionResult CreatePayment()
        {
            List<PersonalAccount> accountCollection = new List<PersonalAccount>();
           
            if (dataBase.Open())
            {
                dataBase.GetTable(accountCollection);
                dataBase.Close();
            }
            ViewBag.accounts = accountCollection;
            return View();
        }

        [HttpPost]
        public IActionResult CreatePayment(PersonalAccountPayment payment)
        {
            List<PersonalAccount> accountCollection = new List<PersonalAccount>();
            List<PersonalAccountPayment> paymentCollection = new List<PersonalAccountPayment>();
            if (dataBase.Open())
            {
                dataBase.AddPersonalAccountPayment(payment);
                dataBase.GetTable(accountCollection);
                dataBase.GetTable(paymentCollection);
                dataBase.Close();
            }
            ViewBag.accounts = accountCollection;
            return View("Payment", paymentCollection);
        }

        public IActionResult PersonalAccountList()
        {
            List<PersonalAccount> accountCollection = new List<PersonalAccount>();
            if (dataBase.Open())
            {
                dataBase.GetTable(accountCollection);
                dataBase.Close();
            }
            return View(accountCollection);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
