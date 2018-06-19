using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.UI.Models;

namespace VideoRentalStoreSystem.UI
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Splash()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Interval = 2000;
            timer.Start();
        }
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            timer.Stop();
            CheckConnection();
            timer.Enabled = false;
        }
        private void CheckConnection()
        {
            try
            {
                using (DBVRContext context = new DBVRContext())
                {
                    if (!context.Database.Exists())
                    {
                        MessageBoxResult result = MessageBox.Show(VRSSMessage.NotFoundDB,
                            "", MessageBoxButton.OK, MessageBoxImage.Information);
                        context.Database.Create();
                        context.Database.Connection.Open();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(VRSSMessage.CanNotConnectDB);
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
