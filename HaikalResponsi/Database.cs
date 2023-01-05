using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace HaikalResponsi
{
    class Database
    {
        public SQLiteConnection db;

        public Database()
        {
            db = new SQLiteConnection(@"Data Source=C:\Users\Haik\Desktop\Haikal Responsi\pemrog.db");
        }

        public void OpenConnection()
        {
            if (db.State != System.Data.ConnectionState.Open)
            {
                db.Open();
            }
        }

        public void CloseConnection()
        {
            if (db.State != System.Data.ConnectionState.Closed)
            {
                db.Close();
            }
        }
    }
}
