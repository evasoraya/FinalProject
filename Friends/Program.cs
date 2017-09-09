// Graph Engine
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
using Friends.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends
{
    class Friends
    {
        private static long answer1 = 7049524;

        public unsafe static void Main(string[] args)
        {

            ActorEntity actor = new ActorEntity("actors", "Actor");
            actor.loadData();

            DirectorEntity director = new DirectorEntity("directors", "Director");
            director.loadData();

            
            MovieEntity movie = new MovieEntity("movies", "Movie");
            movie.loadData();

            
            do
            {
                showMenu();

                ConsoleKey key = Console.ReadKey().Key;
               
                if (key == ConsoleKey.Escape) break;
                
                if(key == ConsoleKey.A)
                {
                    addActor();
                }

                if(key == ConsoleKey.B)
                {
                    addDirector();
                }

                if(key == ConsoleKey.C)
                {
                    addMovie();
                }

                if(key == ConsoleKey.Q)
                {
                    removeActor();
                }
                if (key == ConsoleKey.W)
                {
                    removeDirector();
                }

                if (key == ConsoleKey.E)
                {
                    removeMovie();
                }

                if( key == ConsoleKey.T)
                {
                    queryMagico();
                }
            } while (true);

            
        }

        private static void queryMagico()
        {

            long answerl = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            foreach (var movie in Global.LocalStorage.Movie_Accessor_Selector())
            {
                answerl = 0;
                foreach(var actor in movie.Actors)
                {
                    foreach(var actor2 in movie.Actors)
                    {
                        answerl++;   
                    }
                }
                //Console.WriteLine(movie.ID + " " + answer1);
            }

            stopwatch.Stop();
            Console.WriteLine("Respuesta = " + answer1);
            Console.WriteLine("Tiempo tomado = " + stopwatch.ElapsedMilliseconds);

        }

        private static void removeActor()
        {
            Console.WriteLine("Insertar el ID del actor: ");
            string actorId = Console.ReadLine();
            int id = Int32.Parse(actorId);
            DataManager.getInstance().RemoveActor(id);
        }

        private static void removeDirector()
        {
            Console.WriteLine("Insertar el ID del director: ");
            string directorId = Console.ReadLine();
            int id = Int32.Parse(directorId);
            DataManager.getInstance().RemoveDirector(id);
        }

        private static void removeMovie()
        {
            Console.WriteLine("Insertar el ID de la pelicula: ");
            string movieId = Console.ReadLine();
            int id = Int32.Parse(movieId);
            DataManager.getInstance().RemoveMovie(id);
        }

        private static void addActor()
        {

            Console.WriteLine("Insertar el nombre del actor: ");
            string actorName = Console.ReadLine();
            Actor act = new Actor(Name: actorName);
            DataManager.getInstance().AddActor(act);

        }

        private static void addDirector()
        {

            Console.WriteLine("Insertar el nombre del director: ");
            string directorName = Console.ReadLine();
            Director dir = new Director(Name: directorName);
            DataManager.getInstance().AddDirector(dir);

        }

        private static void showMenu()
        {
            Console.WriteLine("A. Agregar un Actor");
            Console.WriteLine("B. Agregar un Director");
            Console.WriteLine("C. Agregar una Pelicula");


            Console.WriteLine("Q. Borrar un Actor");
            Console.WriteLine("W. Borrar un Director");
            Console.WriteLine("E. Borrar una Pelicula");

            Console.WriteLine("T. QUERY MAGICO");

        }
        private static void addMovie()
        {
            Console.WriteLine("Insertar el nombre de la pelicula: ");
            string MovieName = Console.ReadLine();

            Console.WriteLine("Insertar el año de la pelicula: ");
            string MovieYear = Console.ReadLine();

            Console.WriteLine("Insertar el Idioma de la pelicula: ");
            string MovieLang = Console.ReadLine();

            Console.WriteLine("Insertar el Director de la pelicula: ");
            string MovieDir = Console.ReadLine();
            long idDir = Database.getInstance().findDirByName(MovieDir);


            Console.WriteLine("Insertar el numero de actores a registrar para la pelicula: ");
            String cant = Console.ReadLine();

            Console.WriteLine("Insertar ids de los actores de la pelicula: ");
            List<long> MovActors = new List<long>();
            for (int i=0; i < Int32.Parse(cant); i++)
            {
                String idActor = Console.ReadLine();
                MovActors.Add(Int32.Parse(idActor));
            }
            Movie mov = new Movie(Name: MovieName, Year: MovieYear, Lang: MovieLang, Director : idDir, Actors: MovActors );
            
            DataManager.getInstance().AddMovie(mov);
        }

    }

}

