using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ImportExportNAVObjects
{
    class NAVConnection
    {
        private const string Separator = ";";
        private const string ServerConst = "Server=";
        private const string DatabseConst = "Database=";
        private const string UserIDConst = "User ID=";
        private const string PasswordConst = "Password=";
        private const string IntergratedSecurityConst = "Integrated Security=";

        private string ServerName;
        private string DatabaseName;
        private string UserID;
        private string Password;
        private bool IntegratedSecurity;
        private string ConnectionError;
        
        public NAVConnection(string serverName, string databaseName, string userID = "", string password = "")
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            UserID = userID;
            Password = password;

            if (String.IsNullOrEmpty(userID))
            {
                IntegratedSecurity = true;
            }
            else
            {
                IntegratedSecurity = false;
            }
        }

        public string GetConnectionError()
        {
            return ConnectionError;
        }

        public bool TestConnection()
        {
            string connectionString;

            if (IntegratedSecurity)
            {
                connectionString = String.Concat(ServerConst,ServerName,Separator,DatabseConst,DatabaseName,Separator,
                    IntergratedSecurityConst,IntegratedSecurity.ToString()); 
            }
            else
            {
                connectionString = String.Concat(ServerConst, ServerName, Separator, DatabseConst, DatabaseName, Separator,
                    UserIDConst,UserID,Separator,PasswordConst,Password,Separator,IntergratedSecurityConst, IntegratedSecurity.ToString()); 
            }
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException e)
                {
                    ConnectionError = e.Message; 
                    return false;
                }
            }
        }

    }
}
