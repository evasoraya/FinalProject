using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Friends
{
    public class Database
    {

        #region Singleton
        private static Database _instance;
        public static Database getInstance()
        {
            if (_instance == null)
                _instance = new Database();
            return _instance;
        }
        #endregion

        const string server = "localhost";
        const string port = "5433";
        const string userId = "postgres";
        const string password = "123456";
        const string database = "postgres";

        NpgsqlConnection conn;

        public Database()
        {

            string conn_command = "Server={0}; Port={1}; User Id={2}; Password={3}; Database={4}";
            conn_command = String.Format(conn_command, server, port, userId, password, database);

            conn = new NpgsqlConnection(conn_command);
            conn.Open();

        } 

        public List<long> loadNodeType(Entity entity)
        {

            string query = "SELECT id FROM " + entity.TableName;
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }

        public List<long> getActors(long movieId)
        {
            string query = "SELECT actor_id FROM actors_movie WHERE movie_id = " + movieId;
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<long> answer = new List<long>();

            while (dr.Read())
            {
                long id = dr.GetInt64(0);
                answer.Add(id);
            }

            dr.Close();
            return answer;

        }
        public int findDirByName(String name)
        {
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from Directors where name LIKE @name", conn);

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "name";
            parameter.Value = name + "%";
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            int dirId = -1;

            while (dr.Read())
            {
                dirId = Int32.Parse(dr.GetString(0));
                break;
            }

            dr.Close();
            return dirId;

        }

        public int findActorByName(String name)
        {
            NpgsqlConnection conn = Database.getInstance().Connection;
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id from actors where name LIKE @name", conn);

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "name";
            parameter.Value = name + "%";
            cmd.Parameters.Add(parameter);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            int dirId = -1;

            while (dr.Read())
            {
                dirId = Int32.Parse(dr.GetString(0));
                break;
            }

            dr.Close();
            return dirId;

        }


        public NpgsqlConnection Connection
        {
            get
            {
                return conn;
            }
               
        }

    }
}
