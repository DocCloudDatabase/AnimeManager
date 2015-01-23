using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyLibrary.Controls;
using MyLibrary.Reports;
using MyLibrary.RegUtili;
using MyLibrary.exceptions;
using System.Runtime.InteropServices;

namespace AnimeReport.subelements
{
    
    /// <summary>
    /// [UNTESTED] Report Record Data container 
    /// <para>Ver. : 1.3</para>
    /// <![CDATA[<ul>
    ///            <li> Author : Oleg Sobko </li>
    ///            <li> Version : 1.3 </li>
    ///            <li> Description [ITA] : 
    ///                 <div> Classe che contiene i dati neccessari alla generazione di un report row E' consigliato uso del costruttore completo</div>
    ///            </li>
    ///          </ul>
    /// ]]>
    /// </summary>
    /// <seealso cref="AnimeReport.subelements.RecordRow"/>
    public class ReportRecord
    {
        private string _title;
        private int _available;
        private int _downloaded;
        private string _source;
        private RecordStatus _stats;
        private object _index;


        /// <summary>
        /// Default empty constructor
        /// </summary>
        public ReportRecord()
        :this("Anime Title Here", 0 , 0, "Unknown"){}

        /// <summary>
        /// Default complete constructor. Status = Searching
        /// </summary>
        /// <param name="title"> titolo della anime</param>
        /// <param name="avb"> numero dei episodi disponibili</param>
        /// <param name="down"> numero dei episodi scaricati</param>
        public ReportRecord(string title, int avb, int down, string source)
        : this(title,avb,down, source, RecordStatus.Searching, null) {}

        public ReportRecord(AnimeRecord record)
        {
            this._title = record.title;
            this._available = record.available;
            this._downloaded = record.downloaded;
            this._source = record.source;
            this._stats = (RecordStatus)record.status;
            this._index = record.other;
        }

        public ReportRecord(string title, int avb, int down, string source, RecordStatus status, object aus)
        {
            this._title = title;
            this._available = avb;
            this._downloaded = down;
            this._source = source;
            this._stats = status;
            Ausiliar = aus;
        }

        public string Title
        {
            get
            {
                return this._title;
            }
        }

        public int Available
        {
            get
            {
                return this._available;
            }
        }

        public int Download
        {
            get
            {
                return this._downloaded;
            }
        }

        public string Source
        {
            get
            {
                return this._source;
            }
        }

        public string Status
        {
            get
            {
                if (_index == null) _index = "Uknown";
                switch (_stats)
                {
                    case RecordStatus.DirectDownload:
                        return "Next Direct : " + _index.ToString();
                    case RecordStatus.TorrentDownload:
                        return "Eps in torrent : " + _index.ToString();
                    case RecordStatus.ToMove:
                        return "Move to : " + _index.ToString();
                    default:
                        return _stats.ToString();
                }
            }
        }

        public object Ausiliar
        {
            get
            {
                return this._index;
            }
            set
            {
                this._index = value;
            }
        }

        /// <summary>
        /// Load data by using DOI (Default Object Identifier).
        /// </summary>
        /// <param name="container"> WrapPanel that contain all controls to be filled with data</param>
        /// <exception cref="MyLibrary.exceptions.InvalidConfiguration"> The class failed to initialize data</exception>
        /// <remarks>ver 1.3 : Added Combobox Initializzation</remarks>
        public void Load(ref WrapPanel container)
        {
            //Rule of 1-4 : No Status intialization
            for (int i = 1; i < 5; i++)
            {
                if (!SetTextBox(ref container, (DOI)i)) throw new InvalidConfiguration();
                
            }
            //Initialize status
            SetList(ref container, DOI.Status);
        }

        public static string GetDOI(int cod)
        {
            DOI d = (DOI)cod;
            return d.ToString();
        }

        /// <summary>
        /// Sync data between container -> data. Singleverse
        /// </summary>
        /// <param name="data"> data do update</param>
        /// <param name="container"> container with new data</param>
        /// <returns></returns>
        public static bool Sync(ReportRecord data, WrapPanel container)
        {
            //Rule 1-5
            for (int i = 1; i < 5; i++)
            {
                TextBox box = UContainer.getElementByID<TextBox>(container,
                              UContainer.TYPE_TEXT + "_" + GetDOI(i));
                //Text -> Property
                if (box != null) {
                    if (!data.SetValue(box.Text, (DOI)i)) return false;
                }
                else
                {
                    return false;
                }
            }
            //Getting Combobox value
            ComboBox cbox = UContainer.getElementByID<ComboBox>(container,
                            UContainer.TYPE_LIST + "_Status");
            if (cbox != null)
            {
                data._stats = (RecordStatus)(int)Math.Pow(2, cbox.SelectedIndex);//Syncing ENUM data
                return true;
            }

            return false;
        }

        public static bool CheckValues(ref WrapPanel container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get object resource by doi
        /// </summary>
        /// <param name="doi_cod"> Default Object Identifier (DOI) number</param>
        /// <returns>ver. 1.1 - dynamic loader</returns>
        private object GetValue(DOI cod)
        {
            //rule 1 - 5
            switch (cod)
            {
                case DOI.Title:
                    return this._title;
                case DOI.EpsA:
                    return this._available;
                case DOI.EpsD:
                    return this._downloaded;
                case DOI.Source:
                    return this._source;
                case DOI.Status:
                    return this._stats;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Dynamic value setter, doesn't support Status setting
        /// </summary>
        /// <param name="value"> Value to set</param>
        /// <param name="cod"> Default Object Identifier (DOI)</param>
        /// <returns>
        /// <para>TRUE : setted value sucessfully</para>
        /// <para>FALSE : failed to set value (impossible cast)</para>
        /// </returns>
        /// <remarks>Version 1.1 : Setter result</remarks>
        private bool SetValue(object value, DOI cod)
        {
            switch (cod)
            {
                case DOI.Title:
                    _title = value.ToString();
                    return true;
                case DOI.EpsA:
                    return Int32.TryParse(value.ToString(), out _available);
                case DOI.EpsD:
                    return Int32.TryParse(value.ToString(), out _downloaded);
                case DOI.Source:
                    _source = value.ToString();
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Set a specified field (DOI) value of object
        /// </summary>
        /// <param name="container"> field container</param>
        /// <param name="value"> value to set</param>
        /// <param name="id"> Default Object Identifier of field</param>
        /// <returns></returns>
        private bool SetTextBox(ref WrapPanel container, DOI id)
        {
            return UContainer.SetText(ref container, this.GetValue(id), id.ToString());
        }

        /// <summary>
        /// Imposta il testo della combobox come lo stato corrente del record.
        /// </summary>
        /// <param name="container">Riferimento al container della combobox</param>
        /// <param name="id"> DOI della combobox</param>
        /// <returns>
        /// <para>TRUE: se operazione e' riuscita</para>
        /// <para>FALSE: se operazione e' fallita</para>
        /// </returns>
        /// <remarks>Ver. 1.1 : Corrected minor bugs </remarks>
        private bool SetList(ref WrapPanel container, DOI id)
        {
            ComboBox cbox = UContainer.getElementByID<ComboBox>(container, UContainer.TYPE_LIST + "_" + id.ToString());
            if (cbox != null)
            {
                cbox.SelectedIndex = 1 + (int)Math.Log((int)id, 2);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Load list elements into combobox. Combobox must be indentified with a correct DOI
        /// </summary>
        /// <param name="container"> reference to the container</param>
        /// <returns>
        /// <para>TRUE : List items loaded correctly</para>
        /// <para>FALSE : Failed to load items</para>
        /// </returns>
        public static bool LoadList(ref WrapPanel container)
        {
            ComboBox cbox = UContainer.getElementByID<ComboBox>(container, UContainer.TYPE_LIST + "_" + DOI.Status.ToString());
            if (cbox != null)
            {
                LoadList(ref cbox);
                return true;
            }

            return false;
        }

        public static void LoadList(ref ComboBox cbox)
        {
            ListResource<string> lista = new ListResource<string>(Resources.Default.list_Status, false); //Get resource list
            //UContainer.LoadList(ref cbox, lista.Items.Skip(1).ToArray());
            UContainer.LoadList(ref cbox, UArray.SubArray<string>(lista.Items, 1, lista.Items.Length - 1));
        }

        public static explicit operator AnimeRecord(ReportRecord info){
            AnimeRecord record = new AnimeRecord();
            record.title = info._title;
            record.available = info._available;
            record.downloaded = info._downloaded;
            record.source = info._source;
            record.status = (int)info._stats;
            record.other = info._index;
            return record;
        }

        public static bool AreSame(AnimeRecord left, AnimeRecord right)
        {
            return left.title == right.title;
        }

        internal enum DOI
        {
            Title = 1, //ID for ANime TiTle
            EpsA = 2, //ID for Episodi disponibili
            EpsD = 3, //ID for Episodi scaricati
            Source = 4, //ID for possible source
            Status = 5 //ID for status of serie
        }
    }

    public enum RecordStatus : int{
        Completed = 1,
        Stall = 2,
        Planning = 4,
        Searching = 8,
        DirectDownload = 16,
        TorrentDownload = 32,
        Waiting = 64,
        ToMove = 128
    }

    
    public struct AnimeRecord
    {
        /// <summary>
        /// [size = 256] Title of anime
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string title;
        public int available;
        public int downloaded;
        /// <summary>
        /// [size = 256] Source of anime video
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string source;
        public int status;
        /// <summary>
        /// [size = 128] Addictional information
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public object other;
    }

    public struct ADictionary
    {
        /// <summary>
        /// [Size = 256] Title of anime (index)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string title;
        /// <summary>
        /// Start posizione
        /// </summary>
        public int offset;
    }
}
