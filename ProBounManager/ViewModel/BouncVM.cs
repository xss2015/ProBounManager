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
    class BouncVM : INotifyPropertyChanged
    {
        #region Variables

        List<Filter> filters;
        List<Emp> emps;
        //Filter selectedFilter;
        //Emp selectedEmp;
        string txtSearch;

        List<Result> results;
        List<PromoHistory> promoHistories;
        List<BouncHistory> bouncHistories;
        PromoHistory promoHistory;
        BouncHistory bouncHistory;
        List<Result> multiSelectedResult;
        DateTime filterDate;
        List<Rank> ranks;
        string txtNotes;
        List<PrivilageYear> privilageYears;
        List<PrivilageMonth> privilageMonths;
        List<Stage> stages;
        List<Penalty> penalties;
        #endregion

        #region Properties
        public List<Penalty> Penalties { get { return penalties; } set { if (penalties != value) { penalties = value; OnPropertyChanged("Penalties"); } } }
        public List<Stage> Stages { get { return stages; } set { if (stages != value) { stages = value; OnPropertyChanged("Stages"); } } }
        public List<PrivilageMonth> PrivilageMonths { get { return privilageMonths; } set { if (privilageMonths != value) { privilageMonths = value; OnPropertyChanged("PrivilageMonths"); } } }
        public List<PrivilageYear> PrivilageYears { get { return privilageYears; } set { if (privilageYears != value) { privilageYears = value; OnPropertyChanged("PrivilageYears"); } } }
        public List<Rank> Ranks { get { return ranks; } set { if (ranks != value) { ranks = value; OnPropertyChanged("Ranks"); } } }
        public List<Result> Results { get { return results; } set { if (results != value) { results = value; OnPropertyChanged("Results"); } } }
        public BouncHistory BouncHistory { get { return bouncHistory; } set { if (bouncHistory != value) { bouncHistory = value; OnPropertyChanged("BouncHistory"); } } }
        public string TxtNotes { get { return txtNotes; } set { if (txtNotes != value) { txtNotes = value; OnPropertyChanged("TxtNotes"); } } }
        public PromoHistory PromoHistory { get { return promoHistory; } set { if (promoHistory != value) { promoHistory = value; OnPropertyChanged("PromoHistory"); } } }
        public List<Filter> Filters { get { return filters; } set { if (filters != value) { filters = value; OnPropertyChanged("Filters"); } } }
        public List<Result> MultiSelectedResult { get { return multiSelectedResult; } set { if (multiSelectedResult != value) { multiSelectedResult = value; OnPropertyChanged("MultiSelectedResult"); } } }
        public DateTime FilterDate { get { return filterDate; } set { if (filterDate != value) { filterDate = value; OnPropertyChanged("FilterDate"); } } }
        //public List<Filter> Filters { get { return filters; } set { if (filters != value) { filters = value; OnPropertyChanged("Filters"); } } }
        public List<Emp> Emps { get { return emps; } set { if (emps != value) { emps = value; OnPropertyChanged("Emps"); } } }
        //public Filter SelectedFilter { get { return selectedFilter; } set { if (selectedFilter != value) { selectedFilter = value; OnPropertyChanged("SelectedFilter"); } } }
        //public Emp SelectedEmp { get { return selectedEmp; } set { if (selectedEmp != value) { selectedEmp = value; OnPropertyChanged("SelectedEmp"); } } }
        public string TxtSearch { get { return txtSearch; } set { if (txtSearch != value) { txtSearch = value; ResultSearch(txtSearch); OnPropertyChanged("TxtSearch"); } } }
        public TheCommands ViewCommand { get; set; }
        public TheCommands CrntApplyCommand { get; set; }
        //public TheCommands DelayMonth { get; set; }
        //public TheCommands BouncBlock { get; set; }
        public TheCommands ViewAllCommand { get; set; }
        public DelegateCommand<object> ExportCommand { get; set; }
        #endregion

        #region Functions&Ctor

        public BouncVM()
        {
            filterDate = DateTime.Now;
            OnPropertyChanged("FilterDate");
            //LoadAll();
            ViewCommand = new TheCommands(OnView);
            CrntApplyCommand = new TheCommands(OnCrntApply);
            //DelayMonth = new TheCommands(OnDelayMonth);
            //BouncBlock = new TheCommands(OnBouncBlock);
            ViewAllCommand = new TheCommands(OnViewAll);
            ExportCommand = new DelegateCommand<object>(this.Export);


            CheckStudentCommand = new TheCommands(OnCheckStudent);
            CheckAllStudentsCommand = new TheCommands(OnCheckAllStudents);
            IsAllSelected = false;

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
                    //filters = db.Filters.ToList();
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

                    results = query.Where(x => x.BouncHistory.BouncNextStageDate <= filterDate && x.BouncHistory.BouncNextStageDate >= DateTime.Now).ToList();



                }
                OnPropertyChanged("Results");
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
           
        }
        private void OnViewAll()
        {
            LoadAll();
        }
        private void OnCrntApply()
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
                            int crntStageCount = db.Stages.Where(x => x.StageId == item.BouncHistory.Stage.StageId).Select(n => n.StageCount).Single();

                            bouncHistory = new BouncHistory();

                            bouncHistory.PromoId = item.PromoHistory.PromoId; // id
                            bouncHistory.StageId = db.Stages.Where(x => x.StageCount == item.BouncHistory.Stage.StageCount + 1).Select(n => n.StageId).Single(); // crnt stage
                            bouncHistory.BouncNextStage = db.Stages.Where(x => x.StageId == bouncHistory.StageId + 1).Select(n => n.StageName).Single(); // next stage
                            bouncHistory.BouncCrntStageDate = item.BouncHistory.BouncNextStageDate;// crnt stage date
                            bouncHistory.BouncNextStageDate = bouncHistory.BouncCrntStageDate.AddYears(1);// next stage date

                            db.BouncHistories.Add(bouncHistory);
                            db.SaveChanges();


                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
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
        private void ResultSearch(string p)
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
        private void Export(object grid)
        {
            if (grid != null)
            {
                ToExcelClass.ExportDataBounc((DataGrid)grid);
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
