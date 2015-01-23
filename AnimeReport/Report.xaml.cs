#define SIGNALS
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
using AnimeReport.subelements;
using MyLibrary.mIO;
using MyLibrary.Reports;
using System.ComponentModel;
using MyLibrary.core;

namespace AnimeReport
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgloader;
        public MainWindow()
        {
            InitializeComponent();
            bgloader = new BackgroundWorker();
        }

        /*============= TILE BAR CONTROLS ===============*/
        private void LetsMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LetsExit(object sender, RoutedEventArgs e)
        {
            //Check Report State

            //Exit
            this.Close();
        }

        private void LetsAdd(object sender, RoutedEventArgs e)
        {
            ReportRow row = new ReportRow();
            MainContainer.Children.Add(row);
            //Enable edit mode when add
//            row.AccessObject(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Show file open dialog

            string filepath = "temp.anime";//Here goes filepath

        }

        #region BACKGROUND_WORKER_HANDLERS

        private void LoadInBackground(object sender, DoWorkEventArgs e)
        {
            string filepath = e.Argument.ToString();
            BackgroundWorker bw = sender as BackgroundWorker;
            AnimeRecord[] records = UFile.ReadRecords<AnimeRecord>(filepath);
            int i = 0;

            if (records != null)
            {
#if SIGNALS
                FLogger.Info("File read correctly!");
#endif
                foreach (AnimeRecord record in records)
                {
                    bw.ReportProgress(100 * (i / records.Length), record.title);

                    ReportRow row = new ReportRow(new ReportRecord(record),
                                                  RowStatus.None
                                                 );
                    MainContainer.Children.Add(row);
                    
                    i++;
                }
            }
#if SIGNALS
            else
            {
                FLogger.Warning("Cannot load records from file \"" + filepath + "\"");
            }
#endif
        }
        private void SaveInBackground(object sender, DoWorkEventArgs e)
        {
            string filepath = e.Argument.ToString();
            BackgroundWorker bw = sender as BackgroundWorker;
            AnimeRecord[] records = ReportRow.GetArray(MainContainer, MainContainer.Children.Count);

            bw.ReportProgress(10, "Updating changes...");
            UFile.UpdateRecords<AnimeRecord>(filepath, records, ReportRecord.AreSame);

            bw.ReportProgress(25, "Adding new records");
            /*
             * USING FileStream <- filepath
             *  Attraversa main container e trova gli stati "ToAdd" e "ToDelete"
             *      - ToAdd : esegui write record
             *      - ToDelete : esegui delete record
             * END USING
             */
        }



        #endregion

    }
}
