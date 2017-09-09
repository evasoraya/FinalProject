using Friends.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{
    public class DataManager
    {

        ActorEntity actorEntity;
        DirectorEntity directorEntity;
        MovieEntity movieEntity;

        public DataManager()
        {
            actorEntity = new ActorEntity("actors", "Actor");
            actorEntity.loadData();

            directorEntity = new DirectorEntity("directors", "Director");
            directorEntity.loadData();


            movieEntity = new MovieEntity("movies", "Movie");
            movieEntity.loadData();

        }

        private static DataManager _instance;

        public static DataManager getInstance()
        {
            if (_instance == null)
                _instance = new DataManager();

            return _instance;
        }

        public void AddMovie(Movie m)
        {
            movieEntity.Add(m);
        }

        public void AddActor(Actor a)
        {
            actorEntity.Add(a);
        }

        public void AddDirector(Director d)
        {
            directorEntity.Add(d);
        }

        public void RemoveActor(int ID)
        {
            actorEntity.RemoveActor(ID);
        }

        public void RemoveDirector(int ID)
        {
            directorEntity.RemoveDirector(ID);
        }
        public void RemoveMovie(int ID)
        {
            movieEntity.RemoveMovie(ID);
        }
    }
}
