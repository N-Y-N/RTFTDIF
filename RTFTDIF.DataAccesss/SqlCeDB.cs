using NeotericDev.Db;
using RTFTDIF.Common.Models;
using RTFTDIF.DataAccesss.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using Dapper;
using System.Linq;
using static NeotericDev.Commons.Logger.LogManager;
using System.IO;

namespace RTFTDIF.DataAccesss
{
    public class SqlCeDB : IDisposable
    {
        private SqlCeEngine _SqlCeEngine;

        private readonly static string conString = ConfigurationManager.ConnectionStrings[Constants.Keys.ConnectionName].ConnectionString;

        private SqlCeDB()
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\log4net.config";
                if (File.Exists(path)) {
                    path = "Exists";
                }
                _SqlCeEngine = new SqlCeEngine(conString);
                if (!_SqlCeEngine.Verify())
                {
                    Logger.LogDebug(this, $"Database not found. Creating Database & required tables");
                    String relativeFilePath = ConfigurationManager.AppSettings[Constants.Keys.DbScript];
                    string fullPath = AppDomain.CurrentDomain.BaseDirectory + relativeFilePath;
                    _SqlCeEngine.CreateDatabase();
                    CreateTables(fullPath);
                }
                Logger.LogDebug(this, "Creating SqlCeDB Instance");
            }
            catch (Exception e)
            {
                Logger.LogError(this, "Exception Occured while creating SqlCeDB Instance", e);
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

        public bool SaveCategory(Category category)
        {
            string query = "INSERT INTO Category(Id, Name, CreatedDate, UpdatedDate, IsActive) VALUES(@Id, @CategoryName, @CreatedDate, @UpdatedDate, @IsActive)";
            return ExecuteNonQuery(query, category) > 0;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT C.Id, C.Name CategoryName, C.CreatedDate, C.UpdatedDate, C.IsActive, Count(I.Id) FilesCount, Sum(I.Size) Size FROM Category C LEFT JOIN Item I ON C.Id = I.CategoryId AND I.IsActive = 1 WHERE C.IsActive = 1 GROUP BY C.Id, C.Name, C.CreatedDate, C.UpdatedDate, C.IsActive";
            categories = Query<Category>(query).ToList();
            return categories;
        }

        public bool SaveItems(List<Item> items)
        {
            string query = "INSERT INTO Item(Id, CategoryId, Name, Type, Path, Size, CreatedDate, UpdatedDate, IsActive) VALUES(@Id, @CategoryId, @Name, @Type, @Path, @Size, @CreatedDate, @UpdatedDate, @IsActive)";
            return ExecuteNonQuery(query, items) > 0;
        }

        public List<Item> GetItems(string categoryId)
        {
            List<Item> items = new List<Item>();
            string query = "SELECT Id, CategoryId, Name, Type, Path, Size, CreatedDate, UpdatedDate, IsActive FROM Item WHERE IsActive = 1 AND CategoryId = '" + categoryId + "'";
            items = Query<Item>(query);
            return items;
        }

        public List<Item> GetItems(List<string> ids)
        {
            List<Item> items = new List<Item>();
            string query = "SELECT Id, CategoryId, Name, Type, Path, Size, CreatedDate, UpdatedDate, IsActive FROM Item WHERE IsActive = 1 AND Id IN @Ids";
            var param = new { Ids = ids.Select(x => x).ToArray() };
            items = Query<Item>(query, param);
            return items;
        }

        private int ExecuteNonQuery(string query)
        {
            try 
            {
                using (SqlCeConnection connection = new SqlCeConnection(conString))
                {
                    Logger.LogDebug(this, $"Executing NonQuery : {query}");
                    return connection.Execute(query);
                }
            }
            catch(Exception e)
            {
                Logger.LogError(this, $"Exception occured while executing : {query}", e);
                return -1;
            }
        }

        public int DeleteItems(List<string> deletedFiles)
        {
            string query = "UPDATE Item SET IsActive = 0 WHERE id IN @Ids";
            var param = new { Ids = deletedFiles.ToArray() };
            return ExecuteNonQuery(query, param);
        }

        public int DeleteCategories(List<string> categoryIds)
        {
            string query = "UPDATE Category SET IsActive = 0 WHERE id IN @Ids";
            var param = new { Ids = categoryIds.ToArray() };
            return ExecuteNonQuery(query, param);
        }

        private int ExecuteNonQuery(string query, Object param)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(conString))
                {
                    Logger.LogDebug(this, $"Executing NonQuery : {query}");
                    return connection.Execute(query, param);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(this, $"Exception occured while executing : {query}", e);
                return -1;
            }
        }


        private List<T> Query<T>(string query)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(conString))
                {
                    Logger.LogDebug(this, $"Executing Query : {query}");
                    return connection.Query<T>(query).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(this, $"Exception occured while executing : {query}", e);
                return null;
            }
        }


        private List<T> Query<T>(string query, Object param)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(conString))
                {
                    Logger.LogDebug(this, $"Executing NonQuery : {query}");
                    return connection.Query<T>(query, param).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(this, $"Exception occured while executing : {query}", e);
                return null;
            }
        }

    }
}
