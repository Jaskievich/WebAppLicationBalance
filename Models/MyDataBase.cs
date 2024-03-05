using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Reflection;

namespace WebApplicationBalance.Models
{
    public class MyDataBase
    {
        public static readonly string QueryPersonalAccount          = "SELECT * FROM PersonalAccount";
        public static readonly string QueryPersonalAccountInvoice   = "SELECT PAI.Id, PAI.PersonalAccountId, " +
            "PA.Account, PAI.SumAccount, PAI.Description FROM PersonalAccountInvoice PAI JOIN PersonalAccount PA" +
            " ON PAI.PersonalAccountId = PA.Id";
        public static readonly string QueryPersonalAccountPayment = "SELECT PAP.Id, PAP.PersonalAccountId, " +
          "PA.Account, PAP.SumPayment, PAP.PurposePayment FROM PersonalAccountPayment PAP JOIN PersonalAccount PA" +
          " ON PAP.PersonalAccountId = PA.Id";
        public static readonly string QueryBalance = "Select * from ViewBalance";

        private readonly SqlConnection? connection ;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MyDataBase(string connectionString)
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
                log.Error(ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message);
                return false;
            }
            catch (ConfigurationErrorsException ex)
            {
                log.Error(ex.Message);
                return false;
            }
            return true;
        }

        public void Close()
        {
            connection?.Close();
        }

        public void GetTable(List<PersonalAccount> PersonalAccountCollection)
        {
            SqlCommand command = new SqlCommand(QueryPersonalAccount, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    object Account = reader.GetValue(0);
                    object FullName = reader.GetValue(1);
                    object id = reader.GetValue(2);

                    PersonalAccountCollection.Add(new PersonalAccount((int)id, (string)Account, (string)FullName));

                }
            }
            reader.Close();
        }

        public void GetTable(List<PersonalAccountInvoice> PersonalAccountInvoiceCollection)
        {
            SqlCommand command = new SqlCommand(QueryPersonalAccountInvoice, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    object PersonalAccountId = reader.GetValue(1);
                    object Account = reader.GetValue(2);
                    object sum = reader.GetValue(3);
                    object desc = reader.GetValue(4);

                    PersonalAccountInvoiceCollection.Add(new PersonalAccountInvoice((int)id, (int)PersonalAccountId, (string)Account, 
                        (Decimal)sum, (string)desc));

                }
            }
            reader.Close();
        }

        public void GetTable(List<PersonalAccountPayment> PaymentCollection)
        {
            SqlCommand command = new SqlCommand(QueryPersonalAccountPayment, connection);
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
                    PaymentCollection.Add(new PersonalAccountPayment((int)id, (int)account_id, (string)account, (Decimal)sum, (string)desc));
                }
            }
            reader.Close();
        }

        public void GetTable(List<Balance> BalanceCollection)
        {
            SqlCommand command = new SqlCommand(QueryBalance, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    object account = reader.GetValue(0);
                    object sum = reader.GetValue(1);
                    BalanceCollection.Add(new Balance((string)account, (Decimal)sum));
                }
            }
            reader.Close();
        }

        public void AddPersonalAccountInvoice(PersonalAccountInvoice personalAccountInvoice)
        {
            if (personalAccountInvoice != null)
            {
                string sqlExpression = String.Format("EXECUTE AddPersonalAccountMoney '{0}', {1}, '{2}', 1",
                    personalAccountInvoice.PersonalAccountId, personalAccountInvoice.SumAccount, personalAccountInvoice.Description);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public void AddPersonalAccountPayment(PersonalAccountPayment personalAccountPayment)
        {
            if (personalAccountPayment != null)
            {
                string sqlExpression = String.Format("EXECUTE AddPersonalAccountMoney '{0}', {1}, '{2}', 2", 
                    personalAccountPayment.PersonalAccountId, personalAccountPayment.SumPayment, personalAccountPayment.PurposePayment);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
