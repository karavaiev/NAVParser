using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImportExportNAVObjects
{
    class NAVExport
    {
        private const string Separator = ",";
        private const string CommandConst = "command=exportobjects";
        private const string FileConst = "file=";
        private const string ServernameConst = "servername=";
        private const string DatabaseConst = "database=";
        private const string LogfileConst = "logfile=";
        private const string FilterConst = "filter=";
        private const string UsernameConst = "username=";
        private const string PasswordConst = "password=";
        private const string NTAuthenticationConst = "ntauthentication=";

        private string FinSqlFileName;
        private string FileName;
        private string ServerName;
        private string DatabaseName;
        private string LogFileName;
        private string Filter;
        private string UserName;
        private string Password;
        private byte NTAuthentication;

        public NAVExport(string finsqlFileName, string fileName, string serverName, string databaseName, string logFileName = "", 
            string filter = "", string userName = "", string password = "", byte ntauthentication = 0)
        {
            FinSqlFileName = finsqlFileName;
            FileName = fileName;
            ServerName = serverName;
            DatabaseName = databaseName;
            LogFileName = logFileName;
            Filter = filter;
            UserName = userName;
            Password = password;
            NTAuthentication = ntauthentication;
        }

        public void SetFilter(string typeFilter = "", string idFilter = "", string nameFilter = "", string modifiedFilter = "",
            string compiledFilter = "", string dateFilter = "", string timeFilter = "", string versionListFilter = "",
            string captionFilter = "", string lockedFilter = "", string lockedByFilter = "")
        {
            NAVObjectFilter filter = new NAVObjectFilter(typeFilter,idFilter,nameFilter,modifiedFilter,compiledFilter,dateFilter,
                timeFilter,versionListFilter,captionFilter,lockedFilter,lockedByFilter);

            Filter = filter.GetFilterString();
        }

        private void AddElementToList(ref List<string> stringList, string element)
        {
            if (!String.IsNullOrEmpty(element))
            {
                stringList.Add(element);
            }
        }

        private string[] CreateParamArray()
        {
            List<string> paramList = new List<string>();

            AddElementToList(ref paramList, CommandConst);
            AddElementToList(ref paramList, String.Concat(FileConst,FileName));
            AddElementToList(ref paramList, String.Concat(ServernameConst,ServerName));
            AddElementToList(ref paramList, String.Concat(DatabaseConst,DatabaseName));
            AddElementToList(ref paramList, NAVObjectFilter.NonBlankValue(LogFileName, LogfileConst));
            AddElementToList(ref paramList, NAVObjectFilter.NonBlankValue(Filter, FilterConst));
            AddElementToList(ref paramList, NAVObjectFilter.NonBlankValue(UserName, UsernameConst));
            AddElementToList(ref paramList, NAVObjectFilter.NonBlankValue(Password, PasswordConst));
            AddElementToList(ref paramList, String.Concat(NTAuthenticationConst,NTAuthentication.ToString()));

            return paramList.ToArray();
        }
        
        private string GetParamString()
        {
            return String.Join(Separator, CreateParamArray());
        }

        public void Export()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = FinSqlFileName;
            startInfo.Arguments = GetParamString();

            process.StartInfo = startInfo;
            process.Start();
        }

    }
}
