using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.DAL
{
    public class SqlAccess
    {
        //decale the sql conn
        public SqlConnection sqlConn;

        //connection string

        public string nwConnString = "";

        //step 2: connection, open and close
        public SqlConnection GetConnection()
        {
            //this method will instantiate and return any connection
            //check if the conn is alredy existed else create new one
            if (sqlConn == null)
            {
                sqlConn = new SqlConnection(nwConnString);

            }
            return sqlConn;
        }

        internal void OpenConnection()
        {
            //this method will open sql connection
            //check if the conn is already opened
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
        }

        internal void CloseConnection()
        {
            sqlConn.Close();
        }

        public int ExecuteScalar(string query, SqlCommand cmd)
        {
            SqlConnection sqlCon = null;
            String SqlconString =
              "Data Source=(localdb)\\MSSQLLocalDB;" +
              "Initial Catalog=Geometry;" +
              "Integrated Security=SSPI;";
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand Cmnd = new SqlCommand(query, sqlCon);
                Cmnd.CommandType = CommandType.Text;
                for(int i = 0; i < cmd.Parameters.Count; i++)
                {
                    Cmnd.Parameters.Add(cmd.Parameters[i].ParameterName, cmd.Parameters[i].SqlDbType).Value = cmd.Parameters[i].Value; 
                }
                var result = Cmnd.ExecuteScalar();
                sqlCon.Close();
                return Convert.ToInt32(result);
            }
        }

        public int ExecuteNonQuery(string query, SqlCommand cmd)
        {
            SqlConnection sqlCon = null;
            String SqlconString =
              "Data Source=(localdb)\\MSSQLLocalDB;" +
              "Initial Catalog=Geometry;" +
              "Integrated Security=SSPI;";
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand Cmnd = new SqlCommand(query, sqlCon);
                Cmnd.CommandType = CommandType.Text;
                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    Cmnd.Parameters.Add(cmd.Parameters[i].ParameterName, cmd.Parameters[i].SqlDbType).Value = cmd.Parameters[i].Value;
                }
                var result = Cmnd.ExecuteNonQuery();
                sqlCon.Close();
                return Convert.ToInt32(result);
            }
        }


        public DataSet ExecuteDataSet(string sql)
        {
            String SqlconString =
              "Data Source=(localdb)\\MSSQLLocalDB;" +
              "Initial Catalog=Geometry;" +
              "Integrated Security=SSPI;";
            SqlConnection conn = new SqlConnection(SqlconString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            ///conn.Open();
            da.Fill(ds);
            ///conn.Close();

            return ds;
        }
    }

}

