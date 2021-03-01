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
        public List<Media> mediaList = new List<Media>();

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

            Movie movie = new Movie();

            string[] arr;
            try
            {
                while (!parser.EndOfData)
                {
                    arr = parser.ReadFields();
                    movie.mediaId = UInt64.Parse(arr[0]);
                    movie.title = arr[1];
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

        public void AddMovie(Movie movie)
        {

        }
    }
}