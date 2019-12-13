using NeotericDev.Db;
using RTFTDIF.DataAccesss.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.DataAccesss
{
    public class SqlCeDB : IDisposable
    {
        private SqlCeEngine _SqlCeEngine;
        
        private SqlCeDB()
        {
            string conString = ConfigurationManager.ConnectionStrings[Constants.Keys.ConnectionName].ConnectionString;
            _SqlCeEngine = new SqlCeEngine(conString);
            if (!_SqlCeEngine.Verify()) 
            {
                String relativeFilePath = ConfigurationManager.AppSettings[Constants.Keys.DbScript];
                string fullPath = AppDomain.CurrentDomain.BaseDirectory + relativeFilePath;
                _SqlCeEngine.CreateDatabase();
                CreateTables(fullPath);
            }
        }

        private void CreateTables(string fullPath)
        {
            SqlCeConnection connection = new SqlCeConnection(_SqlCeEngine.LocalConnectionString);
            SqlCeCommand command = new SqlCeCommand();
            var dbInitlzr = new DbInitializer(connection, command);
            dbInitlzr.CreateTables(fullPath);
        }

        public static SqlCeDB NewInstance { get => new SqlCeDB(); }

        public void Dispose()
        {
            _SqlCeEngine.Dispose();
        }        
    }
}
