using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends.Entities
{

    public class ActorEntity : Entity
    {


        public ActorEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {

            Entity entity = (Entity)this;
            List<long> actors = Database.getInstance().loadNodeType(entity);

            foreach(long actorId in actors){                
                Actor act = new Actor(ID: actorId, Movies: new List<long>());
                Global.LocalStorage.SaveActor(act);
            }


        }

        public void Add(Actor act)
        {

            string name = act.Name;
            string arr = "INSERT INTO actors (name) VALUES(@Name)";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();
                act.ID = Database.getInstance().findActorByName(name);

                Global.LocalStorage.SaveActor(act);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Actor getActorByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach(var actor in Global.LocalStorage.Actor_Accessor_Selector())
            {
                if (actor.ID == ID)
                    return actor;

            }
            return null;
        }
        public void RemoveActor(int ID)
        {
            var Actor = getActorByID(ID);

            string arr = "DELETE FROM actors WHERE id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;
                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Actor.CellID);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
