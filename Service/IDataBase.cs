using NuGet.Configuration;
using NuGet.Protocol.Plugins;
using System.Configuration;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

using System.Reflection;
using WebApplicationBalance.Controllers;
using WebApplicationBalance.Models;

namespace WebApplicationBalance.Service
{

    public interface IDataBase
    {         
        bool Open();

        void Close();

        void CreateConnection(string? connectionString);

        public void GetTable<T>(string query, List<T> ItemCollection, dAddItem<T> AddItem );

        public void AddItem(string query);

    }

    public delegate void dAddItem<T>(SqlDataReader reader, List<T> ItemCollection);

    class MyDataBase: IDataBase, IDisposable
    {  
 
        private SqlConnection? connection;

        private readonly ILogger<MyDataBase> _logger;

        public MyDataBase(ILogger<MyDataBase> logger)
        {
           _logger = logger;
           
        }

        public void CreateConnection(string? connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public bool Open()
        {
            try
            {
                // Открываем подключение
                connection?.Open();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (ConfigurationErrorsException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        public void Close()
        {
            connection?.Close();
        }

        public void GetTable<T>(string query, List<T> ItemCollection, dAddItem<T> AddItem)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader? reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            AddItem(reader, ItemCollection);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }

        }

        public void AddItem(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    command.ExecuteNonQuery();

                }catch (SqlException ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            if( connection != null) { connection.Close(); }
        }
    }

    public class MyDataBaseHelper
    {
        public static readonly string QueryPersonalAccount = "SELECT * FROM PersonalAccount";
        public static readonly string QueryPersonalAccountInvoice = "SELECT PAI.Id, PAI.PersonalAccountId, " +
            "PA.Account, PAI.SumAccount, PAI.Description FROM PersonalAccountInvoice PAI JOIN PersonalAccount PA" +
            " ON PAI.PersonalAccountId = PA.Id";
        public static readonly string QueryPersonalAccountPayment = "SELECT PAP.Id, PAP.PersonalAccountId, " +
          "PA.Account, PAP.SumPayment, PAP.PurposePayment FROM PersonalAccountPayment PAP JOIN PersonalAccount PA" +
          " ON PAP.PersonalAccountId = PA.Id";
        public static readonly string QueryBalance = "Select * from ViewBalance";

        private IDataBase _dataBase;

        public MyDataBaseHelper(IDataBase dataBase) { 
            _dataBase = dataBase;
        }

        public bool Open()
        {
           return _dataBase.Open();
        }

        public void Close() { _dataBase.Close(); }

        public void GetTable(List<PersonalAccount> PersonalAccountCollection)
        {
            void AddItemPersonalAccount(SqlDataReader reader, List<PersonalAccount> PersonalAccountCollection)
            {
                object Account = reader.GetValue(0);
                object FullName = reader.GetValue(1);
                object id = reader.GetValue(2);
                PersonalAccountCollection.Add(new PersonalAccount((int)id, (string)Account, (string)FullName));
            }

            _dataBase.GetTable(QueryPersonalAccount, PersonalAccountCollection, AddItemPersonalAccount);
        }

        public void GetTable(List<PersonalAccountInvoice> PersonalAccountInvoiceCollection)
        {
            void AddItemPersonalAccountInvoiceCollection(SqlDataReader reader, List<PersonalAccountInvoice> PersonalAccountInvoiceCollection)
            {
                object id = reader.GetValue(0);
                object PersonalAccountId = reader.GetValue(1);
                object Account = reader.GetValue(2);
                object sum = reader.GetValue(3);
                object desc = reader.GetValue(4);

                PersonalAccountInvoiceCollection.Add(new PersonalAccountInvoice((int)id, (int)PersonalAccountId, (string)Account,
                    (Decimal)sum, (string)desc));
            }

            _dataBase.GetTable(QueryPersonalAccountInvoice, PersonalAccountInvoiceCollection, AddItemPersonalAccountInvoiceCollection);
        }

        public void GetTable(List<PersonalAccountPayment> PaymentCollection)
        {
            void AddItemPersonalAccountPayment(SqlDataReader reader, List<PersonalAccountPayment> PaymentCollection)
            {
                object id = reader.GetValue(0);
                object account_id = reader.GetValue(1);
                object account = reader.GetValue(2);
                object sum = reader.GetValue(3);
                object desc = reader.GetValue(4);
                PaymentCollection.Add(new PersonalAccountPayment((int)id, (int)account_id, (string)account, (Decimal)sum, (string)desc));
            }

            _dataBase.GetTable(QueryPersonalAccountPayment, PaymentCollection, AddItemPersonalAccountPayment);
        }

        public void GetTable(List<Balance> BalanceCollection)
        {
            void AddItemBalance(SqlDataReader reader, List<Balance> BalanceCollection)
            {
                object account = reader.GetValue(0);
                object sum = reader.GetValue(1);
                BalanceCollection.Add(new Balance((string)account, (Decimal)sum));
            }

            _dataBase.GetTable(QueryBalance, BalanceCollection, AddItemBalance);
        }

        public void AddPersonalAccountInvoice(PersonalAccountInvoice personalAccountInvoice)
        {
            if (personalAccountInvoice != null)
            {
                string? sqlExpression = null;
                if (personalAccountInvoice.PersonalAccountId != 0)
                    sqlExpression = String.Format("EXECUTE AddPersonalAccountMoney '{0}', {1}, '{2}', 1", personalAccountInvoice.PersonalAccountId, personalAccountInvoice.SumAccount, personalAccountInvoice.Description);
                else if (personalAccountInvoice.Account.Length != 0)
                    sqlExpression = String.Format("EXECUTE AddPersonalAccountInvoise '{0}', {1}, '{2}'", personalAccountInvoice.Account, personalAccountInvoice.SumAccount, personalAccountInvoice.Description);
                else return;
                _dataBase.AddItem(sqlExpression);
            }
        }

        public void AddPersonalAccountPayment(PersonalAccountPayment personalAccountPayment)
        {
            if (personalAccountPayment != null)
            {
                string sqlExpression = String.Format("EXECUTE AddPersonalAccountMoney '{0}', {1}, '{2}', 2",
                    personalAccountPayment.PersonalAccountId, personalAccountPayment.SumPayment, personalAccountPayment.PurposePayment);
                _dataBase.AddItem(sqlExpression);
            }
        }

        public void AddPersonalAccount(PersonalAccount personalAccount)
        {
            if (personalAccount != null)
            {
                string sqlExpression = personalAccount.Id == 0 ?
                    String.Format("EXECUTE AddPersonalAccount '{0}', '{1}'", personalAccount.Account, personalAccount.FullName) :
                    String.Format("UPDATE  PersonalAccount SET Account = '{0}', FullName = '{1}' WHERE id = {2}", personalAccount.Account, personalAccount.FullName, personalAccount.Id);
                _dataBase.AddItem(sqlExpression);
            }
        }

        public void DelPersonalAccount(int id)
        {
            if (id > 0)
            {
                string sqlExpression = String.Format("DELETE FROM PersonalAccount WHERE id = '{0}'", id);
                _dataBase.AddItem(sqlExpression);
            }
        }


    }
}
