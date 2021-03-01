using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using NLog.Web;
using System.Linq;


namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            if (!File.Exists("movies.scrubbed.csv"))
            {
                string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
                logger.Info(scrubbedFile);
            }

            string filePath = "movies.scrubbed.csv";

            FileReader fr = new FileReader(filePath);

            fr.parseFile();

            Console.WriteLine("1) Add Movie");
            Console.WriteLine("2) Display All Movies");
            Console.WriteLine("Enter to Quit");
            string response = Console.ReadLine();

            if (response == "1")
            {
                //make a new movie
                Movie movie = new Movie();

                //get title
                Console.WriteLine("Enter movie title");
                movie.title = Console.ReadLine();

                if (fr.isTitleUnique(movie.title))
                {
                    string input;
                    //get genres
                    do
                    {
                        Console.WriteLine("Enter genre (or done to quit)");
                        input = Console.ReadLine();

                        if (input != "done" && input.Length > 0)
                        {
                            movie.genres.Add(input);
                        }
                    } while (input != "done");

                    if (movie.genres.Count == 0)
                    {
                        movie.genres.Add("(no genres listed");
                    }

                    //get director
                    Console.WriteLine("Enter movie director");
                    movie.director = Console.ReadLine();

                    //get runtime
                    Console.WriteLine("Enter running time (h:m:s)");
                    movie.runningTime = TimeSpan.Parse(Console.ReadLine());

                    fr.AddMovie(movie);
                }

            }
            else if (response == "2")
            {
                for (var i = 0; i < fr.mediaList.Count; i++)
                {
                    Movie movie = fr.mediaList[i];
                    Console.WriteLine(movie.Display());
                }
            }

            logger.Info("Program ended");
        }
    }
}
