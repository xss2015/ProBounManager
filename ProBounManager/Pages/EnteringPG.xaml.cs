using ProBounManager.Models;
using ProBounManager.ViewModel;
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
    /// Interaction logic for EnteringPG.xaml
    /// </summary>
    public partial class EnteringPG : Page
    {
        public EnteringPG()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.EnteringVM();
        }

        //private void btnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    System.Collections.IList items = (System.Collections.IList)MyGrid.SelectedItems;
        //    var mySelectedlist = items.Cast<Emp>();


        //    using (DBEmp db = new DBEmp())
        //    {
        //        foreach (var item in mySelectedlist)
        //        {
        //            if (item != null)
        //            {
        //                string crntrankBeforeEditing = db.Emps.Where(x => x.EmpName == item.EmpName).Select(z => z.Rank.RankName).FirstOrDefault(); //الرتبة الحالية قبل التعديل
        //                string crntstageBeforeEditing = db.Emps.Where(x => x.EmpName == item.EmpName).Select(z => z.Stage.StageName).FirstOrDefault(); //المرحلة الحالية قبل التعديل
        //                db.Update(item);
        //                int rankCount = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankCount).FirstOrDefault();
        //                if (rankCount == 25)
        //                {
        //                    item.EmpNextRank = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankName).FirstOrDefault(); //الرتبة اللاحقة 
        //                    item.EmpNextRankDate = item.EmpCrntRankDate; //تاريخ الرتبة اللاحقة
        //                }
        //                else
        //                {
        //                    item.EmpNextRank = db.Ranks.Where(x => x.RankId == item.RankId - 1).Select(n => n.RankName).FirstOrDefault();  //الرتبة اللاحقة

        //                    item.EmpNextRankDate = item.EmpCrntRankDate.AddYears(db.Ranks.Where(y => y.RankId == item.RankId).Select(m => m.RankCount).FirstOrDefault());
        //                    item.EmpNextRankDate = item.EmpNextRankDate.AddYears(-db.PrivilageYears.Where(z => z.PrivilageYearId == item.PrivilageYearId).Select(o => o.PrivilageYearCount).FirstOrDefault());
        //                    item.EmpNextRankDate = item.EmpNextRankDate.AddMonths(-db.PrivilageMonths.Where(x => x.PrivilageMonthId == item.PrivilageMonthId).Select(n => n.PrivilageMonthCount).FirstOrDefault());
        //                    item.EmpNextRankDate = item.EmpNextRankDate.AddMonths(db.Penalties.Where(x => x.PenaltyId == item.PenaltyId).Select(n => n.PenaltyCount).FirstOrDefault());
        //                }




        //                item.EmpNextStage = db.Stages.Where(x => x.StageId == item.StageId + 1).Select(n => n.StageName).FirstOrDefault();

        //                item.EmpNextStageDate = item.EmpCrntStageDate.AddYears(1);
        //                item.EmpNextStageDate = item.EmpNextStageDate.AddYears(-db.PrivilageYears.Where(z => z.PrivilageYearId == item.PrivilageYearId).Select(o => o.PrivilageYearCount).FirstOrDefault());
        //                item.EmpNextStageDate = item.EmpNextStageDate.AddMonths(-db.PrivilageMonths.Where(x => x.PrivilageMonthId == item.PrivilageMonthId).Select(n => n.PrivilageMonthCount).FirstOrDefault());
        //                item.EmpNextStageDate = item.EmpNextStageDate.AddMonths(db.Penalties.Where(x => x.PenaltyId == item.PenaltyId).Select(n => n.PenaltyCount).FirstOrDefault());

        //                //#region Edit_PromoHistory
        //                //var EmpInHistory = db.PromoHistories.Where(x => x.PromoHistoryName == item.EmpName && x.PromoHistoryCrntRank == crntrankBeforeEditing).FirstOrDefault();

        //                //if (EmpInHistory != null)
        //                //{
        //                //    EmpInHistory.PromoHistoryName = item.EmpName;
        //                //    EmpInHistory.PromoHistoryCrntRank = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankName).FirstOrDefault();
        //                //    EmpInHistory.PromoHistoryCrntRankDate = item.EmpCrntRankDate;
        //                //}
        //                ////else MessageBox.Show("خطأ", "غير موجود مسبقا", MessageBoxButton.OK, MessageBoxImage.Hand);
        //                //#endregion

        //                //#region Edit_BouncHistory
        //                //var EmpInHistoryBounc = db.BouncHistories.Where(x => x.BouncHistoryName == item.EmpName && x.BouncHistoryRank == crntrankBeforeEditing && x.BouncHistoryCrntStage == crntstageBeforeEditing).ToList();
        //                //if (EmpInHistoryBounc != null)
        //                //{
        //                //    foreach (var item2 in EmpInHistoryBounc)
        //                //    {
        //                //        item2.BouncHistoryName = item.EmpName;
        //                //        item2.BouncHistoryRank = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankName).FirstOrDefault();
        //                //        item2.BouncHistoryCrntStage = db.Stages.Where(x => x.StageId == item.StageId).Select(n => n.StageName).FirstOrDefault();
        //                //        item2.BouncHistoryCrntStageDate = item.EmpCrntStageDate;

        //                //    }
        //                //}
        //                ////else MessageBox.Show("خطأ", "غير موجود مسبقا", MessageBoxButton.OK, MessageBoxImage.Hand);
        //                //#endregion

        //                db.SaveChanges();
        //               //EnteringVM.LoadAll();
        //                //OnPropertyChanged("Emps");

        //            }
        //            else MessageBox.Show("نقص بيانات", "اختر احد العناصر اولا", MessageBoxButton.OK, MessageBoxImage.Hand);
        //        }
        //    }
        //}

        //private void btnEditSelected_Click(object sender, RoutedEventArgs e)
        //{
          
        //        using (DBEmp db = new DBEmp())
        //        {

        //            System.Collections.IList items = (System.Collections.IList)MyGrid.SelectedItems;
        //            var mySelectedlist = items.Cast<Emp>();

        //            foreach (var item in mySelectedlist)
        //            {
        //                if (item != null)
        //                {
        //                    db.Update(item);

        //                    int crntPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == item.PrivilageMonthId).Select(n => n.PrivilageMonthCount).FirstOrDefault(); //عدد اشهر القدم
        //                    int newPrivilageMonth = crntPrivilageMonth + db.PrivilageMonths.Where(x => x.PrivilageMonthId == Convert.ToInt32(cmbPrivilageMonth.Text)).Select(n => n.PrivilageMonthCount).Single();
        //                    int crntPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == item.PrivilageYearId).Select(n => n.PrivilageYearCount).FirstOrDefault();
        //                    int newPrivilageYear = crntPrivilageYear + db.PrivilageYears.Where(x => x.PrivilageYearId == Convert.ToInt32(cmbPrivilageYear.Text)).Select(n => n.PrivilageYearCount).Single();
        //                    int rankCount = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankCount).FirstOrDefault();

        //                    if (rankCount == 25)
        //                    {
        //                        ////item.EmpNextRank = db.Ranks.Where(x => x.RankId == item.RankId).Select(n => n.RankName).FirstOrDefault(); //الرتبة اللاحقة 
        //                        item.EmpNextRankDate = item.EmpCrntRankDate; //تاريخ الرتبة اللاحقة
        //                    }
        //                    else
        //                    {
        //                        ////item.EmpNextRank = db.Ranks.Where(x => x.RankId == item.RankId - 1).Select(n => n.RankName).FirstOrDefault();  //الرتبة اللاحقة

        //                        item.EmpNextRankDate = item.EmpCrntRankDate.AddYears(db.Ranks.Where(y => y.RankId == item.RankId).Select(m => m.RankCount).FirstOrDefault());
        //                        item.EmpNextRankDate = item.EmpNextRankDate.AddYears(-db.PrivilageYears.Where(x => x.PrivilageYearId == item.PrivilageYearId).Select(n => n.PrivilageYearCount).Single()
        //                            - db.PrivilageYears.Where(x => x.PrivilageYearId == Convert.ToInt32(cmbPrivilageYear.Text)).Select(n => n.PrivilageYearCount).Single());
        //                        item.EmpNextRankDate = item.EmpNextRankDate.AddMonths(-db.PrivilageMonths.Where(x => x.PrivilageMonthId == item.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single()
        //                            - db.PrivilageMonths.Where(x => x.PrivilageMonthId == Convert.ToInt32(cmbPrivilageYear.Text)).Select(n => n.PrivilageMonthCount).Single()); //تاريخ الرتبة اللاحقة
        //                    }

        //                    item.EmpNextStageDate = item.EmpCrntStageDate.AddYears(1); //المرحلة اللاحقة
        //                    item.EmpNextStageDate = item.EmpNextStageDate.AddYears(-db.PrivilageYears.Where(z => z.PrivilageYearId == Convert.ToInt32(cmbPrivilageYear.Text)).Select(o => o.PrivilageYearCount).FirstOrDefault());
        //                    item.EmpNextStageDate = item.EmpNextStageDate.AddMonths(-db.PrivilageMonths.Where(x => x.PrivilageMonthId == Convert.ToInt32(cmbPrivilageMonth.Text)).Select(n => n.PrivilageMonthCount).FirstOrDefault());

        //                    item.EmpNextStageDate = item.EmpNextStageDate.AddMonths(-db.PrivilageMonths.Where(x => x.PrivilageMonthId == item.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single()
        //                            - db.PrivilageMonths.Where(x => x.PrivilageMonthId == Convert.ToInt32(cmbPrivilageMonth.Text)).Select(n => n.PrivilageMonthCount).Single());

        //                    item.PrivilageYearId = db.PrivilageYears.Where(x => x.PrivilageYearCount == newPrivilageYear).Select(n => n.PrivilageYearId).Single();
        //                    item.PrivilageMonthId = db.PrivilageMonths.Where(x => x.PrivilageMonthCount == newPrivilageMonth).Select(n => n.PrivilageMonthId).Single();
        //                    //item.PenaltyId = emp.PenaltyId;
        //                    db.SaveChanges();
        //                    //LoadAll();
        //                    //OnPropertyChanged("Emps");

        //                }
        //                else MessageBox.Show("نقص بيانات", "اختر احد العناصر اولا", MessageBoxButton.OK, MessageBoxImage.Hand);
        //            }
                
        //        }

           
            

        //}
    }
}
