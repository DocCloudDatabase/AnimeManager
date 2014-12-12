using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeManager.Enums
{
    public class AnimeType
    {
        private int type;

        public AnimeType(int type)
        {
            this.type = type;
        }

        public AnimeType(string part)
        {
            this.type = getType(part);
        }

        public int Index
        {
            get
            {
                return this.type;
            }
        }


        private static string[] parti = new string[]{
             " OVA ",
             " OVA ",
             " OAD ",
             "MOVIE",
             "FILM",
             " EX ",

        };

        public static int check(String title){
            int ris = -1;

            foreach (String part in parti) {
                if (title.Contains(part))
                {
                    ris = getType(part);
                    break;
                }
            }

            return ris;
        }

        private static int getType(String value)
        {
            int ris = 0;
            for (int i = 0; i < parti.Length; i++)
            {
                if (value.Equals(parti[i].Trim()))
                {
                    ris = i;
                    break;
                }
            }

            return ris;
        }

        public override string ToString()
        {
            if (type >= 0)
            {
                return parti[type].Trim();
            }
            else
            {
                return "Serie";
            }
        }
    }
}
