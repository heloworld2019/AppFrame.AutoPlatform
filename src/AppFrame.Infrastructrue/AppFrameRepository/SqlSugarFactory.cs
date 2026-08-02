using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameRepository
{
    internal class SqlSugarFactory
    {
        private static SqlSugarClient _db = null;
        public static SqlSugarClient Create()
        {
            string path = Environment.CurrentDirectory;
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=" + path + "\\AppFrame.db",
                DbType = DbType.Sqlite,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true
            });
            return _db;
        }
    }
}
