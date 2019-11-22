using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WinFormCRUD
{
    public class DataAccessor
    {
        DatabaseConnectionInfo connectionInfo;
        MySqlConnection connection;


        public DataAccessor(DatabaseConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo;

            string connctionString = connectionInfo.GetConnectionInfo();
            connection = new MySqlConnection(connctionString);
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();

                throw ex;
            }

        }

        public DataSet GetData()
        {
            connection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + connectionInfo.GetTable(), connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            connection.Close();

            return ds;
        }

        public void InsertData(string colNames, string colData)
        {
            string query = "INSERT INTO " + connectionInfo.GetTable() + "(" + colNames + ") VALUES (" + colData + ")";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateData(string cols, string keys)
        {
            string query = "UPDATE " + connectionInfo.GetTable() + " SET " + cols + " WHERE " + keys;

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteData(string keys)
        {
            string query = "DELETE FROM " + connectionInfo.GetTable() + " WHERE " + keys;

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
