using AnimeManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimeManager
{
    /// <summary>
    /// Logica di interazione per AnimeBase.xaml
    /// </summary>
    public partial class AnimeBase : UserControl
    {

        /// <summary>
        /// Costruttore base con info pure
        /// </summary>
        /// <param name="title"> Titolo della serie</param>
        /// <param name="eps"> Episodi disponibili sul dispositivo</param>
        /// <param name="type" ref="AnimeType"> Tipo della serie</param>
        public AnimeBase(string title, int eps, int type)
        {
            InitializeComponent();
            this.Titolo = title;
            this.Disponibili = eps;
            this.Tipo = new AnimeType(type);
            this.Condizione = new SerieCondition(eps, true);
        }

        /// <summary>
        /// Titolo della serie Anime
        /// </summary>
        public string Titolo
        {
            get
            {
                return this.SerieName.Content.ToString();
            }

            set
            {
                this.SerieName.Content = value;
            }
        }

        /// <summary>
        /// Tipo dell'anime
        /// </summary>
        public AnimeType Tipo
        {
            get
            {
                return new AnimeType(SerieType.Content.ToString());
            }
            set
            {
                this.SerieType.Content = value.ToString();
            }
        }

        /// <summary>
        /// Episodi disponibili
        /// </summary>
        public int Disponibili
        {
            get
            {
                return Convert.ToInt32(this.SerieEps.Content);
            }
            set
            {
                this.SerieEps.Content = value.ToString();
            }

        }

        /// <summary>
        /// Stato della serie
        /// </summary>
        public SerieCondition Condizione
        {
            get
            {
                return new SerieCondition(this.SerieStatus.Content.ToString());
            }
            set
            {
                this.SerieStatus.Content = value.ToString();
            }
        }


    }
}
