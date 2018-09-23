using System;
using System.Data;
using System.Data.SqlClient;

namespace Grizzhacks3
{


    class clsSqlConnector
    {
        private SqlConnection con;
        public bool hasSetup = false;

        public void Setup(string dataSource, string initialCatalog, string username, string password)
        {

            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", dataSource, initialCatalog, username, password);
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                con.Close();
                hasSetup = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error connecting to the server, " + e.Message);
            }
        }

        public void Execute(string sqlString)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlString, con);
                cmd.ExecuteScalar();
            }
            catch
            {

            }

            con.Close();
        }

        public string[] GetFirstRow(string sqlString)
        {
            string[] returnData = null;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlString, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                returnData = new string[reader.FieldCount - 1 + 1];

                for (int i = 0; i <= reader.FieldCount - 1; i++)
                    returnData[i] = reader[i].ToString();
            }
            catch
            {

            }


            con.Close();
            return returnData;
        }

        public string GetSingleValue(string sqlString)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string returnData;
            returnData = reader[0].ToString();
            con.Close();
            return returnData;
        }

        public DataTable GetDataTable(string sqlString)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable returnData = new DataTable();
            reader.Fill(returnData);
            con.Close();
            return returnData;
        }
    }
}
