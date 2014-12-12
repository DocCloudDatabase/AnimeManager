using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeManager.core
{
    public class aSerie<T>
    {
        protected string title;
        protected int index; //tralascito
        protected int id_serie;//tralasciato
        protected int eps;
        protected int down;
        protected bool finished;
        protected T type;

        public string Titolo
        {
            get
            {
                return this.title;
            }
        }

        public int Season
        {
            get
            {
                return this.index;
            }
        }

        public int ID
        {
            get
            {
                return this.id_serie;
            }
        }

        public int Disponibili
        {
            get
            {
                return this.eps;
            }
        }

        public int Scaricati
        {
            get
            {
                return this.down;
            }
        }

        public bool isFinished
        {
            get
            {
                return this.finished;
            }
        }

        public T Tipo
        {
            get
            {
                return this.type;
            }
        }

    }
}
