using System.Data.OleDb;
using System.Collections.Generic;
using System;

namespace проект0._1
{
    public class database
    {
        public OleDbConnection Connection { get; set; }

        public database()
        {
            Connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db.mdb;");
        }

        public List<T> ExecuteReader<T>(string query) where T : class
        {
            Connection.Open();
            List<T> list = new List<T>();
            var reader = new OleDbCommand(query, Connection).ExecuteReader();
            while (reader.Read())
            {
                if (typeof(T).Name == "user")
                    list.Add((T)Convert.ChangeType(new user() { Id = (int)reader[0], Login = reader[1].ToString(), Password = reader[2].ToString(), Role = (int)reader[3] }, typeof(T)));
                else if (typeof(T).Name == "product")
                    list.Add((T)Convert.ChangeType(new product() { Id = (int)reader[0], Type = reader[1].ToString(), Quantity = (int)reader[2], CostPerOne = (int)reader[3], Cost = (int)reader[4], Mass = (int)reader[5] }, typeof(T)));
                else if (typeof(T).Name == "sale")
                    list.Add((T)Convert.ChangeType(new sale() { Id = (int)reader[0], Product_type = reader[1].ToString(), Quantity = (int)reader[2], CostPerOne = (int)reader[3], Mass = (int)reader[4], Client = reader[5].ToString(), Operation_type = reader[6].ToString() }, typeof(T)));
            }
            Connection.Close();
            return list;
        }

        public string ExecuteScalar(string query)
        {
            Connection.Open();
            string res = new OleDbCommand(query, Connection).ExecuteScalar().ToString();
            Connection.Close();
            return res;
        }

        public void ExecuteNonQuery(string query)
        {
            Connection.Open();
            new OleDbCommand(query, Connection).ExecuteNonQuery();
            Connection.Close();
        }
    }
}