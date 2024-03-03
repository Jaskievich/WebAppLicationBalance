using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Xml.Linq;
using WebApplicationBalance.Controllers;
namespace WebApplicationBalance.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Display(Name = "Введите счет")]
        [Required(ErrorMessage = "Введите счет")]
        public string Personal_Account {  get; set; }
        [Display(Name = "Введите сумму")]
        [Required(ErrorMessage = "Введите сумму")]
        public Decimal Sum_account {  get; set; }
        [Display(Name = "Введите описание")]
        public string Description { get; set; }
        public Account() { }
        public Account(int id, string Personal_Account, Decimal Sum_account, string Description)
        {
            this.Id = id;
            this.Personal_Account = Personal_Account;   
            this.Sum_account = Sum_account;
            this.Description = Description;
        }
    }

    public class Payment
    {
        public int Id { get; set; }
        [Display(Name = "Выберите лицевой счет")]
        [Required(ErrorMessage = "Выберите лицевой счет")]
        public int Account_Id { get; set; }
        public string Personal_Account { get; set; }
        [Display(Name = "Введите сумму")]
        [Required(ErrorMessage = "Введите сумму")]
        public Decimal Sum_payment { get; set; }
        [Display(Name = "Введите назначение платежа")]
        public string Purpose_payment { get; set; }

        public Payment() { }
        public Payment(int id, int Account_Id, string Personal_Account, Decimal Sum_payment, string Description)
        {
            this.Id = id;
            this.Account_Id = Account_Id;
            this.Personal_Account = Personal_Account;
            this.Sum_payment = Sum_payment;
            this.Purpose_payment = Description;
        }
    }

    public class Balance
    {
        public string Personal_Account { get;  }

        public Decimal Sum_balance { get; }

        public Balance(string Personal_Account, Decimal Sum_balance)
        {
            this.Personal_Account = Personal_Account;
            this.Sum_balance = Sum_balance;
        }

    }

    public class MyDataBase
    {
        private SqlConnection? connection;
        public MyDataBase(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public bool Open()
        {
            if (connection == null) return false;
            try
            {
                // Открываем подключение
                connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }      
            return true;
        }

        public void Close()
        {
            connection?.Close();
        }

        //private void _GetTable<T>(List<T> items, int n_fild, string query )
        //{
        //    SqlCommand command = new SqlCommand(query, connection);
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.HasRows) // если есть данные
        //    {
        //        object [] fields = new object[n_fild];
        //        while (reader.Read()) // построчно считываем данные
        //        {
        //            for(int i = 0; i < n_fild; ++i)
        //            {
        //                fields[i] = reader.GetValue(i);
        //            }
        //            items.Add(new Account((int)id, (string)account, (Decimal)sum, (string)desc));

        //        }
        //    }
        //    reader.Close();
        //}

        public void GetTable(List<Account> accounts)
        {
            SqlCommand command = new SqlCommand("Select * from Accounts", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    object account = reader.GetValue(1);
                    object sum = reader.GetValue(2);
                    object desc = reader.GetValue(3);

                    accounts.Add( new Account((int)id, (string)account, (Decimal)sum, (string)desc) );
                    
                }
            }
            reader.Close();
        }

        public void GetTable(List<Payment> payments)
        {
            const string query = "Select p.id, p.Account_Id, a.Personal_Account, p.Sum_payment, p.Purpose_payment  " +
                "from payments as p join accounts as a on p.Account_Id = a.Id";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    object account_id = reader.GetValue(1);
                    object account = reader.GetValue(2);
                    object sum = reader.GetValue(3);
                    object desc = reader.GetValue(4);
                    payments.Add(new Payment((int)id, (int)account_id, (string)account, (Decimal)sum, (string)desc));
                }
            }
            reader.Close();
        }

        public void GetTable(List<Balance> balances)
        {
            const string query = "Select * from View_balance";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                  
                    object account = reader.GetValue(0);
                    object sum = reader.GetValue(1);
                    balances.Add(new Balance((string)account, (Decimal)sum));

                }
            }
            reader.Close();
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                string sqlExpression = String.Format("INSERT INTO Accounts (Personal_account, Summ_account, Description) VALUES ('{0}', {1}, '{2}')", 
                    account.Personal_Account, account.Sum_account, account.Description);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public void AddPayment(Payment payment)
        {
            if (payment != null)
            {
                string sqlExpression = String.Format("EXECUTE Add_Payment '{0}', {1}, '{2}'", payment.Account_Id, payment.Sum_payment, payment.Purpose_payment);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }

    
}
