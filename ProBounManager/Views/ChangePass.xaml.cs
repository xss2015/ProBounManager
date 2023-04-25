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
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            passOLd.Clear();
            pass1.Clear();
            pass2.Clear();
        }

        private void exitHyper_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void inserHyper_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != null && passOLd.Password != null && pass1.Password != null && pass2.Password != null)
            {
                using (DBEmp db = new DBEmp())
                {
                    var user = db.PWDs.Where(x => x.PWDName == txtName.Text).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.PWDPassword == passOLd.Password)
                        {
                            if (pass1.Password == pass2.Password)
                            {
                                user.PWDPassword = pass2.Password;
                                db.SaveChanges();

                                MessageBox.Show(" تم التعديل بنجاح ");
                               
                            }
                            else
                            {
                                MessageBox.Show("كلمة المرور غير متطابقة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("كلمة المرور القديمة غير صحيحة");
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
    }
}
