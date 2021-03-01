using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;


namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            Console.WriteLine("1) Add Movie");
            Console.WriteLine("2) Display All Movies");
            Console.WriteLine("Enter to Quit");
            string response = Console.ReadLine();

            if (response == "1")
            {
                Movie movie = new Movie();

                Console.WriteLine("Enter movie title");
                movie.title = Console.ReadLine();

                string input;

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
            }
            else if (response == "2")
            {
                //display movies
            }






            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);

            logger.Info("Program ended");
        }
    }
}
