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

namespace ProBounManager.Pages
{
    /// <summary>
    /// Interaction logic for SettingPG.xaml
    /// </summary>
    public partial class SettingPG : Page
    {
        public SettingPG()
        {
            InitializeComponent();
        }

        private void btnRanks_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new RankPG());
        }

        private void btnStages_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new StagePG());
        }

        private void btnPrivlageYear_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new PrivilageYearPG());
        }

        private void btnPrivilageMonth_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new PrivilageMonth());
        }

        private void btnPenalty_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new PenaltyPG());
        }

        private void btnPWD_Click(object sender, RoutedEventArgs e)
        {
            Myframe1.Navigate(new PwdPG());
        }
    }
}
