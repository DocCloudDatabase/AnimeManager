#define SIGNALS
using MyLibrary.RegUtili;
using MyLibrary.Controls;
using MyLibrary.Reports;
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



namespace AnimeReport.subelements
{
    /// <summary>
    /// Logica di interazione per ReportRow.xaml
    /// </summary>
    public partial class ReportRow : UserControl
    {
        /*Constants*/
        private const string EDIT_IN = "Confirm_In.png";
        private const string EDIT_OUT = "Confirm_Out.png";
        private const string NORMAL = "Access.png";

        /*Reports Info*/
        private ReportRecord _info;
        private RowStatus _status;

        /*Flags*/
        private bool _em_ON; //Edit Mode

        public ReportRow()
        : this(new ReportRecord()) { }

        public ReportRow(ReportRecord info)
        : this(info, RowStatus.ToAdd){}

        public ReportRow(ReportRecord info, RowStatus status)
        {
            InitializeComponent();
            this._info = info;
            ReportRecord.LoadList(ref this.Container);
            LoadInfo();
            this._em_ON = false;
            this._status = status;
        }

        internal void AccessObject(object sender, RoutedEventArgs e)
        {
            if (_em_ON)
            {
                //Set flag
                _em_ON = false;

                //Sync, Control and ...
                if (ReportRecord.Sync(_info, Container))
                {
#if SIGNALS
                    FLogger.Info("Data synced!");
#endif
                    //... Disable edit mode
                    SwitchState(false);
                }
                else
                {
                    //.... Report something
#if SIGNALS
                    FLogger.Warning("Failed to sync");
#else
                    throw new NotImplementedException();
#endif
                }
            }else{
                //Set flag
                _em_ON = true;
                //Enable edit mode
                SwitchState(true);
            }
            //Set image
            UMedia.SetBackground<Button>(ref btm_Modify,
                    AnimeReport.Resources.Default.img_folder + 
                    ((_em_ON) ? EDIT_OUT : NORMAL)
                    );
        }

        public static AnimeRecord[] GetArray(WrapPanel container, int count)
        {
            AnimeRecord[] aRecords = new AnimeRecord[count];
            int i = 0;

            foreach (ReportRow row in container.Children)
            {
                aRecords[i] = (AnimeRecord)row.Info;
            }

            return aRecords;
        }


        public ReportRecord Info
        {
            get
            {
                return this._info;
            }
        }

        private void LoadInfo(){
#if SIGNALS
            FLogger.Info("Loading data ...");
#endif
            _info.Load(ref Container);
#if SIGNALS
            FLogger.Info("All data loaded");
#endif
        }

        /// <summary>
        /// Switch edit state of all editable controls
        /// </summary>
        /// <param name="flag"> Edit State to set to all controls</param>
        private void SwitchState(bool flag)
        {
#if SIGNALS
            FLogger.Info("[!]Locking all fields :");
#endif
            //Rule 1-5 : No status switch
            for (int i = 1; i < 5; i++)
            {
                TextBox box = UContainer.getElementByID<TextBox>(this.Container, 
                              UContainer.TYPE_TEXT + "_" + ReportRecord.GetDOI(i));
                if (box != null)
                {
#if SIGNALS
                    FLogger.Info("Field n" + i + "found");
#endif
                    box.IsReadOnly = !flag;
                }
#if SIGNALS
                else{
                    FLogger.Warning("Field n" + i + "not found!");
                }
#endif
            }
            //Status switch
            slc_Status.IsEnabled = flag;
            btm_Cancel.Visibility = (flag) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            btm_Delete.Visibility = (flag) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
#if SIGNALS
            FLogger.Info("Cancel Button (State) : " + btm_Cancel.Visibility.ToString());
            FLogger.Info("Delete Button (State) : " + btm_Delete.Visibility.ToString());
#endif
        }

        private void btm_Cancel_Click(object sender, RoutedEventArgs e)
        {
#if SIGNALS
            FLogger.Info("Editing Canceled. Reloading...");
#endif
            LoadInfo(); //Reload old info
#if SIGNALS
            FLogger.Info("All fields was reseted!");
#endif
            _em_ON = false; //Set flag
            SwitchState(false);//Switch components state            
        }
    }

    public enum RowStatus
    {
        None = 0,
        Updated = 1,
        ToDelete = 2,
        ToAdd = 4
    }
}
