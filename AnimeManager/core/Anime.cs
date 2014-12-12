using AnimeManager.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnimeManager.core
{
    public class Anime
    {
        private static List<string> extensions = new List<string>(new string[] { 
            "mkv",
            "mp4",
            "avi",
            "wmv",
            "mpeg"
        });
        public static int CountEps(string folderpath)
        {
            int ris = 0; //Migliorare con conteggio degli extra
                
            string temp = "";
            string[] files = Directory.GetFiles(folderpath);
            foreach (string filename in files)
            {
                temp = parseOut(filename, ".");
                if (extensions.Contains(temp))
                {
                    ris++;
                }
            }

            return ris;
        }

        public static void AnalizeCollection(ref StackPanel panel, string path)
        {
            List<string> subfolders = new List<string>(Directory.GetDirectories(path));
            subfolders.Sort();
            foreach (string folder in subfolders)
            {
                string title = parseOut(folder, "\\");
                int eps = Anime.CountEps(folder);
                AnimeBase toInsert = new AnimeBase(title, eps, AnimeType.check(title));
                panel.Children.Add(toInsert);
            }
        }

        public static string parseOut(string value, string key)
        {
            int index = 0;
            index = value.LastIndexOf(key);
            return value.Substring(index + 1);
        }
    }
}
