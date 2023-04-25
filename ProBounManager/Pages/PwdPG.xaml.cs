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
    /// Interaction logic for PwdPG.xaml
    /// </summary>
    public partial class PwdPG : Page
    {
        public PwdPG()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.SettingVM();
        }
    }
}
