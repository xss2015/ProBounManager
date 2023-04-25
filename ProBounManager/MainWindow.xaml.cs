using ProBounManager.Models;
using ProBounManager.Views;
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

namespace ProBounManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void changeHyper_Click(object sender, RoutedEventArgs e)
        {
            ChangePass change = new ChangePass();
            change.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainContent main = new MainContent();
                if (txtLoginName.Text != null && txtLoginPass.Password != null)
                {
                    using (DBEmp db = new DBEmp())
                    {

                        var user = db.PWDs.Where(x => x.PWDName == txtLoginName.Text).FirstOrDefault();



                        if (user != null)
                        {
                            if (user.PWDPassword == txtLoginPass.Password)
                            {

                                main.txtUser.Text = txtLoginName.Text;
                                //user.PWDStatus = "yes";
                                //db.PWDs.Update(user);
                                //db.SaveChanges();
                                main.Show();
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("كلمة المرور غير صحيحة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("اسم المستخدم غير صحيح");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء ملاء كافة الحقول");
                }

            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }
    }
}
