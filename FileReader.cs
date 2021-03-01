using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using NLog.Web;
using System.Linq;

namespace MediaLibrary
{
    class FileReader
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public string filePath;
        public List<Movie> mediaList = new List<Movie>();
        private List<string> titles = new List<string>();
        private List<UInt64> mediaIDs = new List<ulong>();

        public FileReader(string filePath)
        {
            this.filePath = filePath;

        }
        public void parseFile()
        {
            //make a text field parser
            TextFieldParser parser = new TextFieldParser(filePath);
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");


            string[] arr;
            try
            {
                while (!parser.EndOfData)
                {
                    Movie movie = new Movie();

                    arr = parser.ReadFields();
                    movie.mediaId = UInt64.Parse(arr[0]);
                    mediaIDs.Add(UInt64.Parse(arr[0]));
                    movie.title = arr[1];
                    titles.Add(arr[1]);
                    movie.genres.Add(arr[2]);
                    movie.director = arr[3];
                    movie.runningTime = TimeSpan.Parse(arr[4]);

                    mediaList.Add(movie);
                }
            }
            catch (Exception ex)
            {
                logger.Info("Something isn't being parsed correctly");
                logger.Info(ex.StackTrace);
            }
        }
        //check for duplicate titles
        public bool isTitleUnique(string title)
        {
            if (titles.ConvertAll(m => m.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }
        public void AddMovie(Movie movie)
        {
            movie.mediaId = mediaIDs.Max() + 1;
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine($"{movie.mediaId},{movie.title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
            sw.Close();
        }
    }
}