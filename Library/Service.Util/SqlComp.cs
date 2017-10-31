using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Util.SQL
{
    class SqlComp
    {
       public static string InsertTable(string tableName, List<SQLField> sqlField)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder field = new StringBuilder();
            StringBuilder value = new StringBuilder();
            sql.Append("insert into " + tableName);
            int index = 1;
            foreach(SQLField f in sqlField)
            {
                field.Append(f.Field+ (index==sqlField.Count? "":","));
                value.Append("'"+f.Value+"'"+ (index == sqlField.Count ? "" : ","));
                index++;
            }
            sql.Append("(" + field + ") values (" + value + ");");
            return sql.ToString();
        }
        public static string UpdateTable(string tableName, List<SQLField> sqlField, List<SQLField> where)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder field = new StringBuilder();
            StringBuilder value = new StringBuilder();
            sql.Append("update " + tableName+" set ");
            
            foreach (SQLField f in sqlField)
            {
                sql.Append(f.Field + "='" + f.Value + "'");
            }

            if (where.Count > 0)
            {
                sql.Append(" where ");
            }
            foreach (SQLField f in where)
            {
                sql.Append(f.Field + "='" + f.Value + "'");
            }
            
            return sql.ToString();
        }
    }
    public class SQLField
    {
        string field;
        string value;

        public string Field
        {
            get
            {
                return field;
            }

            set
            {
                field = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
