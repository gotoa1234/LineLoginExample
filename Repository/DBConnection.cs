using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DBConnection
    {
        public static class SqlDb
        {
            private static string _AzureSqlDb;

            /// <summary>
            /// azure資料庫連線字串
            /// </summary>
            public static string AzureSqlDb
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_AzureSqlDb))
                    {
                        _AzureSqlDb = ConfigurationManager.ConnectionStrings["BochenLinTestEntities"].ConnectionString.ToString();
                    }
                    return _AzureSqlDb;
                }
            }

        }
    }
}
