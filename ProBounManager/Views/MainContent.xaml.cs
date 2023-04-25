using ProBounManager.Models;
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

namespace ProBounManager.Views
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : Window
    {
        public MainContent()
        {
            InitializeComponent();
        }

        private void insertBTN_Click(object sender, RoutedEventArgs e)
        {
            Myframe.Navigate(new Pages.EnteringPG());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnPromo_Click(object sender, RoutedEventArgs e)
        {
            Myframe.Navigate(new Pages.PromoPG());
        }

        private void btnBounc_Click(object sender, RoutedEventArgs e)
        {
            Myframe.Navigate(new Pages.BouncPG());
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            Myframe.Navigate(new Pages.HistoryPG());
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    var user = db.PWDs.Where(x => x.PWDName == txtUser.Text).Single();
                    if (user.PWDStatus == "x")
                    {
                        Myframe.Navigate(new Pages.SettingPG());
                    }
                    else
                    {
                        MessageBox.Show("ليس لديك الصلاحية للدخول لهذه الصفحة", "تحذير");
                    }
                }
            }
            catch (Exception )
            {

                MessageBox.Show("Error");
            }
           
          
        }

      
    }
}
