using System;
using System.Configuration;

namespace BBWS.DAL
{
    public class DalBase
    {
        private static string _connectionString;

        static DalBase()
        {
            _connectionString = "";
        }

        public static string ConnectionString
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    }
                    return _connectionString;
                }
                catch (Exception)
                {
                    throw new Exception("Can't read the connection string");
                }
            }
        }

        public static void SetConnectionString(string specificConnectionString)
        {
            _connectionString = specificConnectionString;
        }
    }
}
