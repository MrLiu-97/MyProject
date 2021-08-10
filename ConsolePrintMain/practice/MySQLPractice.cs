using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrintMain.practice
{
    class MySQLPractice
    {
        public static readonly string connStr = "Server=127.0.0.1;User ID=root;Password=123456;Database=new_schema;CharSet=gbk;";
        public static void Demo1()
        {
            string sqlCmd = "SELECT * FROM new_schema.new_table";
            DataTable dataSet = MySqlQuery(sqlCmd).Tables[0];
            foreach (DataRow item in dataSet.Rows)
            {
                Console.WriteLine("Id:::" + item["Id"] + " Name:::" + item["Name"]);
            }
            List<tbl_Info> tbl_Infos = dataSet.ToList<tbl_Info>();
            Console.WriteLine();
            //SqlDemo();
            //Console.WriteLine("Id:"+dst.Tables[0].Rows[0]["Id"]);
        }
        public class tbl_Info
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private static void SqlDemo()
        {
            MySqlConnector.MySqlConnection con = new MySqlConnector.MySqlConnection(connStr);
            Console.WriteLine(con.State);
            con.Open();
            string sqlCmd = "SELECT * FROM new_schema.new_table";
            MySqlConnector.MySqlCommand comm = new MySqlConnector.MySqlCommand(sqlCmd, con);
            MySqlConnector.MySqlDataAdapter mySqlData = new MySqlConnector.MySqlDataAdapter(comm);

            DataSet dst = new DataSet();
            mySqlData.Fill(dst);
            foreach (DataRow item in dst.Tables[0].Rows)
            {
                Console.WriteLine("Id:::" + item["Id"] + " Name:::" + item["Name"]);
            }
            Console.WriteLine(con.State);
            con.Close();
        }

        /// <summary>
        /// MySQL简单查询
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <returns></returns>
        public static DataSet MySqlQuery(string sqlCmd)
        {

            using (MySqlConnector.MySqlConnection con = new MySqlConnector.MySqlConnection(connStr))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    con.Open();
                    //MySqlConnector.MySqlCommand comm = new MySqlConnector.MySqlCommand(sqlCmd, con);
                    MySqlConnector.MySqlDataAdapter mySqlData = new MySqlConnector.MySqlDataAdapter(sqlCmd, con);
                    mySqlData.Fill(dataSet);
                }
                catch (MySqlConnector.MySqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        /// <summary>
        /// MySQL简单增删
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <returns></returns>
        public static int MySqlExcuteQuery(string sqlCmd)
        {
            using (MySqlConnector.MySqlConnection conn = new MySqlConnector.MySqlConnection(connStr))
            {
                using (MySqlConnector.MySqlCommand command = new MySqlConnector.MySqlCommand(sqlCmd, conn))
                {
                    try
                    {
                        conn.Open();
                        int res = command.ExecuteNonQuery();
                        return res;
                    }
                    catch (MySqlConnector.MySqlException ex)
                    {
                        conn.Close();
                        throw new Exception(ex.Message);
                    }

                }
            }
        }
    }

    public class MyClass
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 主键字段(默认情况下主键字段为ID)
        /// </summary>
        public string KeyName { get; set; }

        private object Save(object theObject, bool isExecute)
        {
            Type type = theObject.GetType();
            StringBuilder sqlPropertyName = new StringBuilder();
            StringBuilder sqlPropertyValue = new StringBuilder();
            PropertyInfo keyPi = null;

            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                bool non = false;
                foreach (var attr in pi.GetCustomAttributes())
                {
                    if (attr is NoPersistentAttribute)
                    {
                        non = true;
                    }
                }
                if (non) continue;

                var propertyName = pi.Name;
                var propertyValue = pi.GetValue(theObject, null);

                if (propertyName.Equals(KeyName, StringComparison.CurrentCultureIgnoreCase))
                {
                    keyPi = pi;
                    continue;
                }

                sqlPropertyName.AppendFormat(",[{0}]", propertyName);
                if (propertyValue == null || propertyValue.ToString() == DateTime.MinValue.ToString())
                {
                    sqlPropertyValue.Append(",null");
                }
                else
                {
                    if (pi.PropertyType == typeof(DateTime))
                    {
                        sqlPropertyValue.AppendFormat(",'{0}'", ((DateTime)propertyValue).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    }
                    else
                    {
                        sqlPropertyValue.AppendFormat(",'{0}'", propertyValue.ToString().Replace("'", "''"));
                    }
                }
            }
            string sql = "INSERT INTO [" + type.Name + "](" + sqlPropertyName.Remove(0, 1) + ")" + " VALUES(" + sqlPropertyValue.Remove(0, 1) + "); SELECT @@IDENTITY";
            if (isExecute)
            {
                object value = ExecuteScalar(sql);
                try
                {
                    if (keyPi != null)
                        keyPi.SetValue(theObject, Convert.ChangeType(value, keyPi.PropertyType), null); //keyPi.SetValue(theObject, value, null);
                }
                catch
                {
                }
                return value;
            }
            else
            {
                return sql;
            }
        }


        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    object obj = comm.ExecuteScalar();
                    return Equals(obj, DBNull.Value) ? null : obj;
                }
            }
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NoPersistentAttribute : System.Attribute
    {

    }

    public static class ExtentClass
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataSet ds) where T : class, new()
        {
            List<T> li = new List<T>();
            if (ds == null && ds.Tables.Count > 0) throw new ArgumentNullException("ds");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li.Add(From<T>(dr));
            }
            return li;
        }
        /// <summary>
        /// 穿件实例类List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            List<T> li = new List<T>();
            if (dt == null) throw new ArgumentNullException("dt");
            foreach (DataRow dr in dt.Rows)
            {
                li.Add(From<T>(dr));
            }
            return li;
        }
        /// <summary>
        /// 创建实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="dr">包含实例类数据的行</param>
        /// <returns></returns>
        public static T From<T>(DataRow dr) where T : class
        {
            if (dr == null) throw new ArgumentException("dr");
            T obj = Activator.CreateInstance<T>();
            if (dr != null)
            {
                //dic = dic ?? new Dictionary<string, object>();
                //if (!dic.ContainsKey(obj.ToString()))
                //{ 
                //    dic.Add(obj.ToString(), );
                //} 
                PropertyInfo[] props = obj.GetType().GetProperties();
                //PropertyInfo[] props ;//= obj.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    //DataMapFieldAttribute mapAttr = (DataMapFieldAttribute)Attribute.GetCustomAttribute(p, typeof(DataMapFieldAttribute));
                    string mapName = p.Name;// "";//要映射的字段名
                    //if (mapAttr != null)
                    //{
                    //    mapName = mapAttr.ColumnName;
                    //}
                    //else
                    //{
                    //    mapName = p.Name;
                    //}
                    if (dr.Table.Columns.Contains(mapName))
                    {
                        if (dr[mapName] != DBNull.Value)
                        {
                            if (dr.Table.Columns[mapName].DataType == p.PropertyType || dr.Table.Columns[mapName].DataType.BaseType == p.PropertyType.BaseType)//可空数据类型的时候不完全相等
                            {
                                var val = dr[mapName];
                                //switch (val.GetType().ToString())
                                var typeName = p.PropertyType.GenericTypeArguments.Length > 0 ? p.PropertyType.GenericTypeArguments[0].FullName : val.GetType().ToString();
                                switch (typeName)
                                {
                                    case "System.Double":
                                        val = Math.Round(Convert.ToDouble(val), 4);
                                        break;
                                    case "System.Decimal":
                                        val = Math.Round(Convert.ToDecimal(val), 4);
                                        break;
                                    case "System.Int32":
                                        val = Convert.ToInt32(val);
                                        break;
                                }
                                p.SetValue(obj, val, null);
                            }
                            else
                            {
                                throw new Exception(string.Format("实体类字段【{0}】类型与数据【{1}】类型不一致！", p.Name, mapName));
                            }
                        }
                    }
                }
            }
            return obj;
        }
    }
}
