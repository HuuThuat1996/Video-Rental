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
using System.Windows.Shapes;
using VideoRentalStoreSystem.BLL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.UI.Models;

namespace VideoRentalStoreSystem.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow mainWindow;
        public delegate void delegateGetParent(Window win);
        public delegateGetParent getParent;
        private string admin = "admin";
        private string employee = "employee";
        private static int isOpen = 0;
        public LoginWindow()
        {
            InitializeComponent();
            getParent = new delegateGetParent(SetParent);
            tbxUserName.Focus();
        }
        private void SetParent(Window win)
        {
            mainWindow = (MainWindow)win;
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SecurityService.Login(tbxUserName.Text, pbx.Password);
            if (!SecurityService.IsLogin() && !SecurityService.IsBlock())
            {
                MessageBox.Show(VRSSMessage.MessageNum23);
                tbxUserName.Focus();
                tbxUserName.SelectAll();
                return;
            }
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SecurityService.IsLogin())
            {
                mainWindow.checkLogin(1);
            }
            else if (SecurityService.IsBlock())
                mainWindow.checkLogin(-1);
            else
            {
                mainWindow.checkLogin(0);
            }
        }
    }
}
