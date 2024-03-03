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
            List<Account> accounts = new List<Account>();
            if (dataBase.Open())
            {
                dataBase.GetTable(accounts);
                dataBase.Close();
            }
            return View(accounts);
        }

        public IActionResult Payment()
        {
            List<Payment> payments = new List<Payment>();
            if (dataBase.Open())
            {
                dataBase.GetTable(payments);
                dataBase.Close();
            }
            return View(payments);
        }

        public IActionResult Balance()
        {
            List<Balance> balances = new List<Balance>();
            if (dataBase.Open())
            {
                dataBase.GetTable(balances);
                dataBase.Close();
            }
            return View(balances);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            List<Account> accounts = new List<Account>();
            if (dataBase.Open())
            {
                dataBase.AddAccount(account);
                dataBase.GetTable(accounts);
                dataBase.Close();
            }
            return View("Index", accounts);
        }

        [HttpGet]
        public IActionResult CreatePayment()
        {
            List<Account> accounts = new List<Account>();
            if (dataBase.Open())
            {
                dataBase.GetTable(accounts);
                dataBase.Close();
            }
            ViewBag.accounts = accounts;
            return View();
        }

        [HttpPost]
        public IActionResult CreatePayment(Payment payment)
        {
            List<Payment> payments = new List<Payment>();
            if (dataBase.Open())
            {
                dataBase.AddPayment(payment);
                dataBase.GetTable(payments);
                dataBase.Close();
            }
            return View("Payment", payments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
