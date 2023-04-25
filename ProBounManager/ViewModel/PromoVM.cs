using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using ProBounManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProBounManager.ViewModel
{
    class PromoVM : INotifyPropertyChanged
    {

        #region Variables

        List<Filter> filters;
        List<Emp> emps;
        List<Result> results;
        Filter selectedFilter;
        Emp selectedEmp;
        List<BouncHistory> bouncHistories;
        List<PromoHistory> promoHistories;
        List<PrivilageYear> privilageYears;
        List<PrivilageMonth> privilageMonths;
        PromoHistory promoHistory;
        BouncHistory bouncHistory;
        List<Rank> ranks;
        List<Stage> stages;
        List<Penalty> penalties;
        List<Result> multiSelectedResult;
        string txtSearch;
        string txtNotes;
        DateTime filterDate;
        #endregion 

        #region Properties
    
        public List<PromoHistory> PromoHistories { get { return promoHistories; } set { if (promoHistories != value) { promoHistories = value; OnPropertyChanged("PromoHistories"); } } }
        public List<BouncHistory> BouncHistories { get { return bouncHistories; } set { if (bouncHistories != value) { bouncHistories = value; OnPropertyChanged("BouncHistories"); } } }
        public BouncHistory BouncHistory { get { return bouncHistory; } set { if (bouncHistory != value) { bouncHistory = value; OnPropertyChanged("BouncHistory"); } } }
        public string TxtNotes { get { return txtNotes; } set { if (txtNotes != value) { txtNotes = value;OnPropertyChanged("TxtNotes"); } } }
        public PromoHistory PromoHistory { get { return promoHistory; } set { if (promoHistory != value) { promoHistory = value; OnPropertyChanged("PromoHistory"); } } }
        public List<Filter> Filters { get { return filters; } set { if (filters != value) { filters = value; OnPropertyChanged("Filters"); } } }
        public List<Result> MultiSelectedResult { get { return multiSelectedResult; } set { if (multiSelectedResult != value) { multiSelectedResult = value; OnPropertyChanged("MultiSelectedResult"); } } }
        public List<Result> Results { get { return results; } set { if (results != value) { results = value; OnPropertyChanged("Results"); } } }
        public DateTime FilterDate { get { return filterDate; } set { if (filterDate != value) { filterDate = value; OnPropertyChanged("FilterDate"); } } }
        public List<PrivilageYear> PrivilageYears { get { return privilageYears; } set { if (privilageYears != value) { privilageYears = value; OnPropertyChanged("PrivilageYears"); } } }
        public List<Rank> Ranks { get { return ranks; } set { if (ranks != value) { ranks = value; OnPropertyChanged("Ranks"); } } }
        public List<Stage> Stages { get { return stages; } set { if (stages != value) { stages = value; OnPropertyChanged("Stages"); } } }
        public List<Penalty> Penalties { get { return penalties; } set { if (penalties != value) { penalties = value; OnPropertyChanged("Penalties"); } } }
        public List<PrivilageMonth> PrivilageMonths { get { return privilageMonths; } set { if (privilageMonths != value) { privilageMonths = value; OnPropertyChanged("PrivilageMonths"); } } }
        public List<Emp> Emps { get { return emps; } set { if (emps != value) { emps = value; OnPropertyChanged("Emps"); } } }
        public Filter SelectedFilter { get { return selectedFilter; } set { if (selectedFilter != value) { selectedFilter = value; OnPropertyChanged("SelectedFilter"); } } }
        public Emp SelectedEmp { get { return selectedEmp; } set { if (selectedEmp != value) { selectedEmp = value; OnPropertyChanged("SelectedEmp"); } } }
        public string TxtSearch { get { return txtSearch; } set { if (txtSearch != value) { txtSearch = value; ResultSearch(txtSearch); OnPropertyChanged("TxtSearch"); } } }
        public TheCommands ViewCommand { get; set; }
        public TheCommands SelectedCrntApplyCommand { get; set; }
        //public TheCommands DelayMonth { get; set; }
        public TheCommands ViewAllCommand { get; set; }
        public DelegateCommand<object> ExportCommand { get; private set; }
        #endregion

        #region Functions
        public PromoVM()
        {

            try
            {
                filterDate = DateTime.Now;
                OnPropertyChanged("FilterDate");
                //LoadAll();
                ViewCommand = new TheCommands(OnView);
                SelectedCrntApplyCommand = new TheCommands(OnSelectedCrntApply);
                ViewAllCommand = new TheCommands(OnViewAll);
                this.ExportCommand = new DelegateCommand<object>(this.Export);

                CheckStudentCommand = new TheCommands(OnCheckStudent);
                CheckAllStudentsCommand = new TheCommands(OnCheckAllStudents);
                IsAllSelected = false;

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
     
        public void LoadAll()
        {
            try
            {
                using (DBEmp db = new DBEmp())

                {
                    emps = db.Emps.ToList();
                    promoHistories = db.PromoHistories.ToList();
                    bouncHistories = db.BouncHistories.ToList();

                    privilageMonths = db.PrivilageMonths.ToList();
                    privilageYears = db.PrivilageYears.ToList();
                    ranks = db.Ranks.ToList();
                    stages = db.Stages.ToList();
                    penalties = db.Penalties.ToList();
                    filters = db.Filters.ToList();
                    filterDate = DateTime.Now;
                    OnPropertyChanged("FilterDate");
                    var temp1 = promoHistories.GroupBy(x => x.EmpId).Select(x => new PromoHistory()
                    {

                        EmpId = x.Key,
                        Rank = x.OrderByDescending(i => i.PromoId).First().Rank,
                        Emp = x.OrderByDescending(i => i.PromoId).First().Emp,
                        PromoCrntRankDate = x.OrderByDescending(i => i.PromoId).First().PromoCrntRankDate,
                        PromoCrntCareer = x.OrderByDescending(i => i.PromoId).First().PromoCrntCareer,
                        PromoNextRank = x.OrderByDescending(i => i.PromoId).First().PromoNextRank,
                        PromoNextRankDate = x.OrderByDescending(i => i.PromoId).First().PromoNextRankDate,
                        PromoNextCareer = x.OrderByDescending(i => i.PromoId).First().PromoNextCareer,
                        PromoId = x.OrderByDescending(i => i.PromoId).First().PromoId,
                        //BouncHistories = x.OrderByDescending(i => i.BouncHistories).First().BouncHistories

                    });

                    var temp2 = bouncHistories.GroupBy(x => x.PromoId).Select(x => new BouncHistory()
                    {
                        PromoId = x.Key,
                        Stage = x.OrderByDescending(i => i.BouncHistoryId).First().Stage,
                        BouncCrntStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncCrntStageDate,
                        BouncNextStage = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStage,
                        BouncNextStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStageDate,
                        BouncHistoryId = x.OrderByDescending(i => i.BouncHistoryId).First().BouncHistoryId,
                        PromoHistory = x.OrderByDescending(i => i.BouncHistoryId).First().PromoHistory
                    });

                    var query = temp1.Join(emps, t1 => t1.EmpId, em => em.EmpId,
                        (t1, em) => new { t1, em }).
                        Join(temp2, t1 => t1.t1.PromoId, t2 => t2.PromoId,
                        (t1, t2) => new { t1, t2 })
                        .Select(m => new Result
                        {
                            Emp = m.t1.em,
                            PromoHistory = m.t1.t1,
                            BouncHistory = m.t2,

                        });

                    results = query.ToList();
                    OnPropertyChanged("Results");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        private void OnView()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    emps = db.Emps.Include(z => z.PrivilageMonth).Include(c => c.PrivilageYear).Include(b => b.Penalty).ToList();
                    promoHistories = db.PromoHistories.Include(z => z.Rank).Include(c => c.Emp).ToList();
                    bouncHistories = db.BouncHistories.Include(x => x.Stage).ToList();

                    var temp1 = promoHistories.GroupBy(x => x.EmpId).Select(x => new PromoHistory()
                    {

                        EmpId = x.Key,
                        Rank = x.OrderByDescending(i => i.PromoId).First().Rank,
                        Emp = x.OrderByDescending(i => i.PromoId).First().Emp,
                        PromoCrntRankDate = x.OrderByDescending(i => i.PromoId).First().PromoCrntRankDate,
                        PromoCrntCareer = x.OrderByDescending(i => i.PromoId).First().PromoCrntCareer,
                        PromoNextRank = x.OrderByDescending(i => i.PromoId).First().PromoNextRank,
                        PromoNextRankDate = x.OrderByDescending(i => i.PromoId).First().PromoNextRankDate,
                        PromoNextCareer = x.OrderByDescending(i => i.PromoId).First().PromoNextCareer,
                        PromoId = x.OrderByDescending(i => i.PromoId).First().PromoId,
                        //BouncHistories = x.OrderByDescending(i => i.BouncHistories).First().BouncHistories

                    });

                    var temp2 = bouncHistories.GroupBy(x => x.PromoId).Select(x => new BouncHistory()
                    {
                        PromoId = x.Key,
                        Stage = x.OrderByDescending(i => i.BouncHistoryId).First().Stage,
                        BouncCrntStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncCrntStageDate,
                        BouncNextStage = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStage,
                        BouncNextStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStageDate,
                        BouncHistoryId = x.OrderByDescending(i => i.BouncHistoryId).First().BouncHistoryId,
                        PromoHistory = x.OrderByDescending(i => i.BouncHistoryId).First().PromoHistory
                    });

                    var query = temp1.Join(emps, t1 => t1.EmpId, em => em.EmpId,
                        (t1, em) => new { t1, em }).
                        Join(temp2, t1 => t1.t1.PromoId, t2 => t2.PromoId,
                        (t1, t2) => new { t1, t2 })
                        .Select(m => new Result
                        {
                            Emp = m.t1.em,
                            PromoHistory = m.t1.t1,
                            BouncHistory = m.t2,

                        });

                    results = query.Where(x => x.PromoHistory.PromoNextRankDate <= filterDate && x.PromoHistory.PromoNextRankDate >= DateTime.Now).ToList();



                }
                OnPropertyChanged("Results");
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
         

        }
        void OnViewAll()
        {
            LoadAll();
        }
        private void OnSelectedCrntApply()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    multiSelectedResult = results.Where(x => x.CheckSelected == true).ToList();

                    if (multiSelectedResult.Count != 0)
                    {
                        foreach (var item in multiSelectedResult)
                        {
                            int crntRankCount = db.Ranks.Where(x => x.RankId == item.PromoHistory.Rank.RankId).Select(n => n.RankCount).Single();
                            int nextRankCount = db.Ranks.Where(x => x.RankName == item.PromoHistory.PromoNextRank).Select(n => n.RankCount).Single();
                            int crntStageCount = db.Stages.Where(x => x.StageId == item.BouncHistory.Stage.StageId).Select(n => n.StageCount).Single();
                            

                            if (crntRankCount == 25)
                            {
                                if (MessageBox.Show(string.Format("الموظف {0} قد وصل لاخر رتبة ... هل تريد الاستمرار للموظف التالي ", item.Emp.EmpName), "تنبيه", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.No)
                                {
                                    return;
                                }



                            }

                            promoHistory = new PromoHistory();
                            bouncHistory = new BouncHistory();

                            promoHistory.EmpId = item.Emp.EmpId; // id
                            promoHistory.RankId = db.Ranks.Where(x => x.RankName == item.PromoHistory.PromoNextRank).Select(n => n.RankId).Single(); // crntRank
                            promoHistory.PromoCrntRankDate = item.PromoHistory.PromoNextRankDate;// crntRankDate
                            if (nextRankCount == 25)
                            {
                                promoHistory.PromoNextRank = db.Ranks.Where(x => x.RankId == promoHistory.RankId).Select(n => n.RankName).Single(); // nextRank
                                promoHistory.PromoNextRankDate = promoHistory.PromoCrntRankDate; //nextRankDate
                                promoHistory.PromoCrntCareer = item.PromoHistory.PromoNextCareer; // crnt career
                                promoHistory.PromoNextCareer = item.PromoHistory.PromoNextCareer; // next career

                            }
                            else
                            {
                                promoHistory.PromoNextRank = db.Ranks.Where(x => x.RankId == promoHistory.RankId - 1).Select(n => n.RankName).Single(); // next rank
                                promoHistory.PromoNextRankDate = promoHistory.PromoCrntRankDate.AddYears(nextRankCount); // next rank date
                                promoHistory.PromoCrntCareer = item.PromoHistory.PromoNextCareer; // crnt career
                            }


                            db.PromoHistories.Add(promoHistory);
                            db.SaveChanges();

                            if (crntStageCount > 11)
                            {
                                bouncHistory.PromoId = promoHistory.PromoId; // id
                                bouncHistory.StageId = db.Stages.Where(x => x.StageCount == crntStageCount - 11).Select(n => n.StageId).Single(); // crnt stage
                                bouncHistory.BouncNextStage = db.Stages.Where(x => x.StageId == bouncHistory.StageId + 1).Select(n => n.StageName).Single(); // next stage
                                bouncHistory.BouncCrntStageDate = promoHistory.PromoCrntRankDate;// crnt stage date
                                bouncHistory.BouncNextStageDate = bouncHistory.BouncCrntStageDate.AddYears(1);// next stage date

                                db.BouncHistories.Add(bouncHistory);
                                db.SaveChanges();

                            }
                            else
                            {
                                bouncHistory.PromoId = promoHistory.PromoId; //
                                bouncHistory.StageId = 1; //
                                bouncHistory.BouncNextStage = db.Stages.Where(x => x.StageCount == bouncHistory.StageId + 1).Select(n => n.StageName).Single(); // 
                                bouncHistory.BouncCrntStageDate = promoHistory.PromoCrntRankDate;//
                                bouncHistory.BouncNextStageDate = bouncHistory.BouncCrntStageDate.AddYears(1);//
                                db.BouncHistories.Add(bouncHistory);
                                db.SaveChanges();
                            }


                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
                            tempEmp.PrivilageYearId = 1;
                            tempEmp.PrivilageMonthId = 1;
                            tempEmp.PenaltyId = 1;
                            tempEmp.EmpNote = TxtNotes;
                            db.SaveChanges();
                        }
                        LoadAll();
                    }

                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        void ResultSearch(string p)
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {

                    //emps = db.Emps.Include(x => x.PrivilageMonth).Include(x => x.PrivilageYear).Include.Where(x => x.EmpName.Contains(p)).ToList();
                    promoHistories = db.PromoHistories.Include(v => v.Rank).Include(c => c.Emp).Where(x => x.Emp.EmpName.Contains(p)).ToList();
                    bouncHistories = db.BouncHistories.Include(c => c.Stage).ToList();
                    var temp1 = promoHistories.GroupBy(x => x.EmpId).Select(x => new PromoHistory()
                    {

                        EmpId = x.Key,
                        Rank = x.OrderByDescending(i => i.PromoId).First().Rank,
                        Emp = x.OrderByDescending(i => i.PromoId).First().Emp,
                        PromoCrntRankDate = x.OrderByDescending(i => i.PromoId).First().PromoCrntRankDate,
                        PromoCrntCareer = x.OrderByDescending(i => i.PromoId).First().PromoCrntCareer,
                        PromoNextRank = x.OrderByDescending(i => i.PromoId).First().PromoNextRank,
                        PromoNextRankDate = x.OrderByDescending(i => i.PromoId).First().PromoNextRankDate,
                        PromoNextCareer = x.OrderByDescending(i => i.PromoId).First().PromoNextCareer,
                        PromoId = x.OrderByDescending(i => i.PromoId).First().PromoId,
                        //BouncHistories = x.OrderByDescending(i => i.BouncHistories).First().BouncHistories

                    });

                    var temp2 = bouncHistories.GroupBy(x => x.PromoId).Select(x => new BouncHistory()
                    {
                        PromoId = x.Key,
                        Stage = x.OrderByDescending(i => i.BouncHistoryId).First().Stage,
                        BouncCrntStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncCrntStageDate,
                        BouncNextStage = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStage,
                        BouncNextStageDate = x.OrderByDescending(i => i.BouncHistoryId).First().BouncNextStageDate,
                        BouncHistoryId = x.OrderByDescending(i => i.BouncHistoryId).First().BouncHistoryId,
                        PromoHistory = x.OrderByDescending(i => i.BouncHistoryId).First().PromoHistory
                    });

                    var query = temp1.Join(emps, t1 => t1.EmpId, em => em.EmpId,
                        (t1, em) => new { t1, em }).
                        Join(temp2, t1 => t1.t1.PromoId, t2 => t2.PromoId,
                        (t1, t2) => new { t1, t2 })
                        .Select(m => new Result
                        {
                            Emp = m.t1.em,
                            PromoHistory = m.t1.t1,
                            BouncHistory = m.t2,

                        });

                    results = query.ToList();

                    OnPropertyChanged("Results");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        private void Export(object gridExcel)
        {
            if (gridExcel != null)
            {
                ToExcelClass.ExportDataPromo((DataGrid)gridExcel);
            }
        }
        #endregion

        #region check

        bool? isAllSelected;
        public bool? IsAllSelected
        {
            get { return isAllSelected; }
            set { isAllSelected = value; OnPropertyChanged("IsAllSelected"); }
        }

        public TheCommands CheckStudentCommand { get; private set; }
        public TheCommands CheckAllStudentsCommand { get; private set; }

        private void OnCheckAllStudents()
        {
            if (IsAllSelected == true)
                results.ForEach(x => x.CheckSelected = true);
            else
                results.ForEach(x => x.CheckSelected = false);
        }

        private void OnCheckStudent()
        {
            if (results.All(x => x.CheckSelected))
                IsAllSelected = true;
            else if (results.All(x => !x.CheckSelected))
                IsAllSelected = false;
            else
                IsAllSelected = null;
        }
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
