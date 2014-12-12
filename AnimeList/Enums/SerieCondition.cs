using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeManager.Enums
{
    public class SerieCondition
    {
        private int condition;


        public SerieCondition(string value)
        {
            this.condition = Reverse(value);
        }
        /// <summary>
        /// Indica la condizione della serie
        /// </summary>
        /// <param name="parameter"> indica <1>indice della condizione o <2>numero degli episodi</param>
        /// <param name="auto">
        /// <para>True - verra eseguita un'analisi automatica e "parameter" sara considerato <2></para>
        /// <para>False - non verra eseguita nessuna analisi e "parameter" sara considerato <1></para>
        /// </param>
        public SerieCondition(int parameter, bool auto)
        {
            if (auto)
            {
                this.condition = Guess(parameter);
            }
            else
            {
                this.condition = parameter;
            }
        }

        private static List<string> condizioni = new List<string>(new string[]{
           "In Corso",
           "Completa",
           "In Stallo",
           "Droppata"
        });

        public int Index
        {
            get
            {
                return this.condition;
            }
        }

        public static int Guess(int eps)
        {
            if (eps < 12)
            {
                return 0;
            }

            if (eps == 13 || eps == 12)
            {
                return 1;
            }

            if (eps > 12 && eps < 24)
            {
                return 0;
            }

            return 1;
        }

        public static int Reverse(string val)
        {
            return condizioni.IndexOf(val);
        }

        public override string ToString()
        {
            try
            {
                return condizioni[condition];
            }catch(IndexOutOfRangeException){
                return "";
            }
        }
    }
}
