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
    class EnteringVM : INotifyPropertyChanged
    {
        #region Variables


        Emp emp;
        List<Emp> emps;
        Emp selectedEmp;
        List<PrivilageMonth> privilageMonths;
        List<PrivilageYear> privilageYears;
        List<Penalty> penalties;
        List<Rank> ranks;
        List<Stage> stages;
        string txtSearch;
        PrivilageMonth selectedPrivilageMonth;
        Result selectedResult;
        List<Result> multiSelectedEmp;
        List<BouncHistory> bouncHistories;
        List<PromoHistory> promoHistories;
        PromoHistory promoHistory;
        BouncHistory bouncHistory;
        Result result;
        List<Result> results;
        #endregion

        #region Properties
        public Result Result { get { return result; } set { if (result != value) { result = value; OnPropertyChanged("Result"); } } }
        public List<Result> Results { get { return results; } set { if (results != value) { results = value; OnPropertyChanged("Results"); } } }
        public Emp Emp { get { return emp; } set { if (emp != value) { emp = value; OnPropertyChanged("Emp"); } } }
        public List<Emp> Emps { get { return emps; } set { if (emps != value) { emps = value; OnPropertyChanged("Emps"); } } }
        public Emp SelectedEmp { get { return selectedEmp; } set { if (selectedEmp != value) { selectedEmp = value; OnPropertyChanged("SelectedEmp"); } } }
        public List<PrivilageMonth> PrivilageMonths { get { return privilageMonths; } set { if (privilageMonths != value) { privilageMonths = value; OnPropertyChanged("PrivilageMonths"); } } }
        public List<PrivilageYear> PrivilageYears { get { return privilageYears; } set { if (privilageYears != value) { privilageYears = value; OnPropertyChanged("PrivilageYears"); } } }
        public List<Penalty> Penalties { get { return penalties; } set { if (penalties != value) { penalties = value; OnPropertyChanged("Penalties"); } } }
        public List<Rank> Ranks { get { return ranks; } set { if (ranks != value) { ranks = value; OnPropertyChanged("Ranks"); } } }
        public List<Stage> Stages { get { return stages; } set { if (stages != value) { stages = value; OnPropertyChanged("Stages"); } } }
        public string TxtSearch { get { return txtSearch; } set { if (txtSearch != value) { txtSearch = value; SearchResult(txtSearch); OnPropertyChanged("TxtSearch"); } } }
        public PromoHistory PromoHistory { get { return promoHistory; } set { if (promoHistory != value) { promoHistory = value; OnPropertyChanged("PromoHistory"); } } }
        public PrivilageMonth SelectedPrivilageMonth { get { return selectedPrivilageMonth; } set { if (selectedPrivilageMonth != value) { selectedPrivilageMonth = value; OnPropertyChanged("SelectedPrivilageMonth"); } } }
        public List<Result> MultiSelectedEmp { get { return multiSelectedEmp; } set { if (multiSelectedEmp != value) { multiSelectedEmp = value; OnPropertyChanged("MultiSelectedEmp"); } } }
        public BouncHistory BouncHistory { get { return bouncHistory; } set { if (bouncHistory != value) { bouncHistory = value; OnPropertyChanged("BouncHistory"); } } }
        public Result SelectedResult { get { return selectedResult; } set { if (selectedResult != value) { selectedResult = value; OnPropertyChanged("SelectedResult"); } } }

        public TheCommands EditCommand { get; set; }
        public TheCommands DelCommand { get; set; }
        public TheCommands AddCommand { get; set; }
      
        public DelegateCommand<object> ExportCommand { get; private set; }
      
        public TheCommands EditPrivilageCommand { get; set; }
        public TheCommands AddPrivilageCommand { get; set; }
        public TheCommands AddPenaltyCommand { get; set; }
        public TheCommands EditPenaltyCommand { get; set; }
        #endregion

        #region Functions&Ctor


        public EnteringVM()
        {
            try
            {

                //CreateDateBase();
                emp = new Emp();
                result = new Result();
                promoHistory = new PromoHistory();
                bouncHistory = new BouncHistory();
                LoadAll();
                EditCommand = new TheCommands(OnEdit);
                DelCommand = new TheCommands(OnDel);
                AddCommand = new TheCommands(OnAdd);

                EditPrivilageCommand = new TheCommands(OnEditPrivilage);
                AddPrivilageCommand = new TheCommands(OnAddPrivilage);
                AddPenaltyCommand = new TheCommands(OnAddPenalty);
                EditPenaltyCommand = new TheCommands(OnEditPenalty);

                CheckStudentCommand = new TheCommands(OnCheckStudent);
                CheckAllStudentsCommand = new TheCommands(OnCheckAllStudents);
                IsAllSelected = false;

                this.ExportCommand = new DelegateCommand<object>(this.Export);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }



        }
        //private void CreateDateBase()
        //{
        //    DBEmp db = new DBEmp();
        //}
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

                    emp.PrivilageYearId = 1;
                    emp.PrivilageMonthId = 1;
                    emp.PenaltyId = 1;
                    emp.EmpDateHiring = DateTime.Now;
                    promoHistory.PromoCrntRankDate = DateTime.Now;
                    bouncHistory.BouncCrntStageDate = DateTime.Now;
                    OnPropertyChanged("Emp");
                    OnPropertyChanged("PromoHistory");
                    OnPropertyChanged("BouncHistory");
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
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        private void OnEdit()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    if (selectedResult != null)
                    {
                        var tempEmp = selectedResult.Emp;
                        tempEmp.PromoHistories = null;
                        db.Update(tempEmp);
                        db.SaveChanges();

                        #region UpdatePromo

                        var tempPromo = selectedResult.PromoHistory;
                        tempPromo.BouncHistories = null;
                        tempPromo.Emp = null;
                        db.PromoHistories.Update(tempPromo);

                        int rankCount = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankCount).Single();  // get the count of current rank
                                                                                                                              //int stageCount = db.Stages.Where(x => x.StageId == selectedResult.BouncHistory.StageId).Select(n => n.StageCount).Single();  //get the count of current stage
                                                                                                                              //int privilageYearCount = db.PrivilageYears.Where(x => x.PrivilageYearId == tempEmp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single(); // get the count of the privilageYear
                                                                                                                              //int privilageMonthCount = db.PrivilageMonths.Where(x => x.PrivilageMonthId == tempEmp.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();  //get the count of the PrivilageMonth
                                                                                                                              //int PenaltyCount = db.Penalties.Where(x => x.PenaltyId == tempEmp.PenaltyId).Select(n => n.PenaltyCount).Single(); // get the count of the penalty Month

                        if (rankCount == 25)
                        {
                            tempPromo.PromoNextRank = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankName).Single();
                            //tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate;
                        }
                        else
                        {
                            tempPromo.PromoNextRank = db.Ranks.Where(x => x.RankId == tempPromo.RankId - 1).Select(n => n.RankName).Single();
                            //tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate.AddYears(rankCount);
                            //tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddYears(-privilageYearCount);
                            //tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(-privilageMonthCount);
                            //tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(PenaltyCount);
                        }
                        if (tempPromo.PromoNextRankDate <= DateTime.Now)
                        {
                            MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                        }
                        db.SaveChanges();

                        #endregion

                        #region UpdateBounc

                        var tempBounc = selectedResult.BouncHistory;
                        tempBounc.PromoHistory = null;
                        db.BouncHistories.Update(tempBounc);

                        tempBounc.BouncNextStage = db.Stages.Where(x => x.StageCount == tempBounc.StageId + 1).Select(n => n.StageName).Single();
                        //tempBounc.BouncNextStageDate = tempBounc.BouncCrntStageDate.AddYears(1);
                        //tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddYears(-privilageYearCount);
                        //tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(-privilageMonthCount);
                        //tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(PenaltyCount);
                        if (tempBounc.BouncNextStageDate <= DateTime.Now)
                        {
                            MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                        }
                        db.SaveChanges();

                        #endregion

                        LoadAll();
                        OnPropertyChanged("Results");

                    }

                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        private void OnDel()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {

                    db.Remove(selectedResult.Emp);
                    db.SaveChanges();
                    LoadAll();
                    OnPropertyChanged("Results");
                
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        private void OnAdd()
        {
            try
            {
                result.Emp = new Emp();

                using (DBEmp db = new DBEmp())
                {
                    if (Emp.EmpName != null)
                    {
                        int rankCount = db.Ranks.Where(x => x.RankId == promoHistory.RankId).Select(n => n.RankCount).Single();  // get the count of current rank
                        int stageCount = db.Stages.Where(x => x.StageId == bouncHistory.StageId).Select(n => n.StageCount).Single();  //get the count of current stage
                        int privilageYearCount = db.PrivilageYears.Where(x => x.PrivilageYearId == emp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single(); // get the count of the privilageYear
                        int privilageMonthCount = db.PrivilageMonths.Where(x => x.PrivilageMonthId == emp.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();  //get the count of the PrivilageMonth
                        int PenaltyCount = db.Penalties.Where(x => x.PenaltyId == emp.PenaltyId).Select(n => n.PenaltyCount).Single(); // get the count of the penalty Month


                        db.Add(emp);
                        db.SaveChanges();

                        #region AddpromoHistory
                        //promoHistory = new PromoHistory();
                        promoHistory.EmpId = emp.EmpId;
                        promoHistory.PromoCrntCareer = promoHistory.PromoCrntCareer;
                        promoHistory.PromoCrntRankDate = promoHistory.PromoCrntRankDate;
                        promoHistory.PromoNextCareer = promoHistory.PromoNextCareer;
                        promoHistory.RankId = promoHistory.RankId;
                        if (rankCount == 25)
                        {
                            promoHistory.PromoNextRank = db.Ranks.Where(x => x.RankId == promoHistory.RankId).Select(n => n.RankName).Single(); // the same next rank
                            promoHistory.PromoNextRankDate = promoHistory.PromoCrntRankDate; // the same crnt rank date
                        }
                        else
                        {
                            promoHistory.PromoNextRank = db.Ranks.Where(x => x.RankId == promoHistory.RankId - 1).Select(n => n.RankName).Single(); // the next rank
                            promoHistory.PromoNextRankDate = promoHistory.PromoCrntRankDate.AddYears(rankCount); // add years of the rank;
                            promoHistory.PromoNextRankDate = promoHistory.PromoNextRankDate.AddYears(-privilageYearCount);
                            promoHistory.PromoNextRankDate = promoHistory.PromoNextRankDate.AddMonths(-privilageMonthCount);
                            promoHistory.PromoNextRankDate = promoHistory.PromoNextRankDate.AddMonths(PenaltyCount);


                        }


                        if (promoHistory.PromoNextRankDate <= DateTime.Now)
                        {
                            MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                        }

                        //if (promoHistory.PromoNextRankDate <= DateTime.Now)
                        //{
                        //    if (MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... هل تريد الاستمرار", "تنبيه", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
                        //    {
                        //        db.Add(promoHistory);
                        //        db.SaveChanges();
                        //    }
                        //    else
                        //    {
                        //        emp = new Emp();
                        //        promoHistory = new PromoHistory();
                        //        bouncHistory = new BouncHistory();
                        //        OnPropertyChanged("Emp");
                        //        OnPropertyChanged("PromoHistory");
                        //        OnPropertyChanged("BouncHistory");
                        //        LoadAll();
                        //        OnPropertyChanged("Results");
                        //        return;
                        //    }

                        //}
                        // else
                        //{
                        db.Add(promoHistory);
                        db.SaveChanges();
                        //}

                        #endregion

                        #region AddbouncHistory
                        //bouncHistory = new BouncHistory();
                        bouncHistory.PromoId = promoHistory.PromoId;
                        bouncHistory.StageId = bouncHistory.StageId;
                        bouncHistory.BouncCrntStageDate = bouncHistory.BouncCrntStageDate;
                        bouncHistory.BouncNextStage = db.Stages.Where(x => x.StageCount == bouncHistory.StageId + 1).Select(n => n.StageName).Single(); // the next Stage
                        bouncHistory.BouncNextStageDate = bouncHistory.BouncCrntStageDate.AddYears(1);
                        bouncHistory.BouncNextStageDate = bouncHistory.BouncNextStageDate.AddYears(-privilageYearCount);
                        bouncHistory.BouncNextStageDate = bouncHistory.BouncNextStageDate.AddMonths(-privilageMonthCount);
                        bouncHistory.BouncNextStageDate = bouncHistory.BouncNextStageDate.AddMonths(PenaltyCount);


                        if (bouncHistory.BouncNextStageDate <= DateTime.Now)
                        {
                            MessageBox.Show("تاريخ العلاوة اقل من تاريخ اليوم ... ", "تنبيه");
                        }

                        //if (bouncHistory.BouncNextStageDate <= DateTime.Now)
                        //{
                        //    if (MessageBox.Show("تاريخ العلاوة اقل من تاريخ اليوم ... هل تريد الاستمرار", "تنبيه", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
                        //    {
                        //        db.Add(bouncHistory);
                        //        db.SaveChanges();
                        //    }
                        //    else
                        //    {
                        //        emp = new Emp();
                        //        promoHistory = new PromoHistory();
                        //        bouncHistory = new BouncHistory();
                        //        OnPropertyChanged("Emp");
                        //        OnPropertyChanged("PromoHistory");
                        //        OnPropertyChanged("BouncHistory");
                        //        LoadAll();
                        //        OnPropertyChanged("Results");
                        //        return;
                        //    }

                        //}
                        //else
                        //{
                        db.Add(bouncHistory);
                        db.SaveChanges();
                        //}

                        #endregion


                        emp = new Emp();
                        promoHistory = new PromoHistory();
                        bouncHistory = new BouncHistory();
                        OnPropertyChanged("Emp");
                        OnPropertyChanged("PromoHistory");
                        OnPropertyChanged("BouncHistory");
                        LoadAll();
                        OnPropertyChanged("Results");
                    }





                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }






        }
        void OnEditPrivilage()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    multiSelectedEmp = results.Where(x => x.CheckSelected == true).ToList();

                    if (multiSelectedEmp.Count != 0)
                    {
                        foreach (var item in multiSelectedEmp)
                        {
                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
                            tempEmp.EmpNote = emp.EmpNote;
                            db.SaveChanges();
                            var tempPromo = item.PromoHistory;
                            tempPromo.BouncHistories = null;
                            tempPromo.Emp = null;
                            db.PromoHistories.Update(tempPromo);
                            var tempBounc = item.BouncHistory;
                            tempBounc.PromoHistory = null;
                            db.BouncHistories.Update(tempBounc);

                            #region PromoPrivilage

                            int rankCount = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankCount).Single();
                            int crntPenalty = db.Penalties.Where(x => x.PenaltyId == tempEmp.PenaltyId).Select(n => n.PenaltyCount).Single();
                            int newPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == emp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single();
                            int newPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == emp.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();





                            if (rankCount == 25)
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate;
                            }
                            else
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate.AddYears(rankCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddYears(-newPrivilageYear);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(-newPrivilageMonth);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(crntPenalty);


                            }

                            tempEmp.PrivilageYearId = db.PrivilageYears.Where(x => x.PrivilageYearCount == newPrivilageYear).Select(x => x.PrivilageYearId).Single();
                            tempEmp.PrivilageMonthId = db.PrivilageMonths.Where(x => x.PrivilageMonthCount == newPrivilageMonth).Select(n => n.PrivilageMonthId).Single();

                            if (tempPromo.PromoNextRankDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }

                            db.SaveChanges();


                            #endregion

                            #region BouncPrivilage

                            tempBounc.BouncNextStageDate = tempBounc.BouncCrntStageDate.AddYears(1);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddYears(-newPrivilageYear);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(-newPrivilageMonth);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(crntPenalty);

                            if (tempBounc.BouncNextStageDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }

                            db.SaveChanges();


                            #endregion


                        }
                        isAllSelected = false;
                        OnPropertyChanged("IsAllSelected");
                        LoadAll();
                        OnPropertyChanged("Results");
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


        }
        void OnAddPrivilage()
        {
            try
            {
                using (DBEmp db = new DBEmp())

                {
                    multiSelectedEmp = results.Where(x => x.CheckSelected == true).ToList();

                    if (multiSelectedEmp.Count != 0)
                    {
                        foreach (var item in multiSelectedEmp)
                        {
                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
                            tempEmp.EmpNote = emp.EmpNote;
                            db.SaveChanges();
                            var tempPromo = item.PromoHistory;
                            tempPromo.BouncHistories = null;
                            tempPromo.Emp = null;
                            db.PromoHistories.Update(tempPromo);
                            var tempBounc = item.BouncHistory;
                            tempBounc.PromoHistory = null;
                            db.BouncHistories.Update(tempBounc);

                            #region PromoPrivilage

                            int rankCount = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankCount).Single();
                            int crntPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == tempEmp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single();
                            int crntPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == tempEmp.PrivilageMonth.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();
                            int crntPenalty = db.Penalties.Where(x => x.PenaltyId == tempEmp.PenaltyId).Select(n => n.PenaltyCount).Single();
                            int newPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == emp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single();
                            int newPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == emp.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();
                            int privilageYearCount = crntPrivilageYear + newPrivilageYear;
                            int privilageMonthCount = crntPrivilageMonth + newPrivilageMonth;





                            if (rankCount == 25)
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate;
                            }
                            else
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate.AddYears(rankCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddYears(-privilageYearCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(-privilageMonthCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(crntPenalty);


                            }

                            tempEmp.PrivilageYearId = db.PrivilageYears.Where(x => x.PrivilageYearCount == privilageYearCount).Select(x => x.PrivilageYearId).Single();
                            tempEmp.PrivilageMonthId = db.PrivilageMonths.Where(x => x.PrivilageMonthCount == privilageMonthCount).Select(n => n.PrivilageMonthId).Single();

                            if (tempPromo.PromoNextRankDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }

                            db.SaveChanges();


                            #endregion

                            #region BouncPrivilage

                            tempBounc.BouncNextStageDate = tempBounc.BouncCrntStageDate.AddYears(1);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddYears(-privilageYearCount);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(-privilageMonthCount);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(crntPenalty);


                            if (tempBounc.BouncNextStageDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }


                            db.SaveChanges();


                            #endregion


                        }
                        isAllSelected = false;
                        OnPropertyChanged("IsAllSelected");
                        LoadAll();
                        OnPropertyChanged("Results");
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
           

        }
        void OnAddPenalty()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    multiSelectedEmp = results.Where(x => x.CheckSelected == true).ToList();

                    if (multiSelectedEmp.Count != 0)
                    {
                        foreach (var item in multiSelectedEmp)
                        {
                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
                            tempEmp.EmpNote = emp.EmpNote;
                            db.SaveChanges();
                            var tempPromo = item.PromoHistory;
                            tempPromo.BouncHistories = null;
                            tempPromo.Emp = null;
                            db.PromoHistories.Update(tempPromo);
                            var tempBounc = item.BouncHistory;
                            tempBounc.PromoHistory = null;
                            db.BouncHistories.Update(tempBounc);

                            #region PromoPrivilage

                            int rankCount = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankCount).Single();
                            int crntPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == tempEmp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single();
                            int crntPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == tempEmp.PrivilageMonth.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();
                            int crntPenalty = db.Penalties.Where(x => x.PenaltyId == tempEmp.PenaltyId).Select(n => n.PenaltyCount).Single();
                            int newPenalty = db.Penalties.Where(x => x.PenaltyId == emp.PenaltyId).Select(n => n.PenaltyCount).Single();
                            int penaltyCount = crntPenalty + newPenalty;





                            if (rankCount == 25)
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate;
                            }
                            else
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate.AddYears(rankCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddYears(-crntPrivilageYear);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(-crntPrivilageMonth);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(penaltyCount);


                            }


                            tempEmp.PenaltyId = db.Penalties.Where(x => x.PenaltyCount == penaltyCount).Select(n => n.PenaltyId).Single();

                            if (tempPromo.PromoNextRankDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }

                            db.SaveChanges();


                            #endregion

                            #region BouncPrivilage

                            tempBounc.BouncNextStageDate = tempBounc.BouncCrntStageDate.AddYears(1);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddYears(-crntPrivilageYear);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(-crntPrivilageMonth);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(penaltyCount);


                            if (tempBounc.BouncNextStageDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }


                            db.SaveChanges();


                            #endregion

                        }
                        isAllSelected = false;
                        OnPropertyChanged("IsAllSelected");
                        LoadAll();
                        OnPropertyChanged("Results");
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        void OnEditPenalty()
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    multiSelectedEmp = results.Where(x => x.CheckSelected == true).ToList();

                    if (multiSelectedEmp.Count != 0)
                    {
                        foreach (var item in multiSelectedEmp)
                        {
                            var tempEmp = item.Emp;
                            tempEmp.PromoHistories = null;
                            db.Emps.Update(tempEmp);
                            tempEmp.EmpNote = emp.EmpNote;
                            db.SaveChanges();
                            var tempPromo = item.PromoHistory;
                            tempPromo.BouncHistories = null;
                            tempPromo.Emp = null;
                            db.PromoHistories.Update(tempPromo);
                            var tempBounc = item.BouncHistory;
                            tempBounc.PromoHistory = null;
                            db.BouncHistories.Update(tempBounc);

                            #region PromoPrivilage

                            int rankCount = db.Ranks.Where(x => x.RankId == tempPromo.RankId).Select(n => n.RankCount).Single();
                            int crntPrivilageYear = db.PrivilageYears.Where(x => x.PrivilageYearId == tempEmp.PrivilageYearId).Select(n => n.PrivilageYearCount).Single();
                            int crntPrivilageMonth = db.PrivilageMonths.Where(x => x.PrivilageMonthId == tempEmp.PrivilageMonth.PrivilageMonthId).Select(n => n.PrivilageMonthCount).Single();
                            int newPenalty = db.Penalties.Where(x => x.PenaltyId == emp.PenaltyId).Select(n => n.PenaltyCount).Single();





                            if (rankCount == 25)
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate;
                            }
                            else
                            {
                                tempPromo.PromoNextRankDate = tempPromo.PromoCrntRankDate.AddYears(rankCount);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddYears(-crntPrivilageYear);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(-crntPrivilageMonth);
                                tempPromo.PromoNextRankDate = tempPromo.PromoNextRankDate.AddMonths(newPenalty);


                            }


                            tempEmp.PenaltyId = db.Penalties.Where(x => x.PenaltyCount == newPenalty).Select(n => n.PenaltyId).Single();


                            if (tempPromo.PromoNextRankDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }


                            db.SaveChanges();


                            #endregion

                            #region BouncPrivilage

                            tempBounc.BouncNextStageDate = tempBounc.BouncCrntStageDate.AddYears(1);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddYears(-crntPrivilageYear);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(-crntPrivilageMonth);
                            tempBounc.BouncNextStageDate = tempBounc.BouncNextStageDate.AddMonths(newPenalty);


                            if (tempBounc.BouncNextStageDate <= DateTime.Now)
                            {
                                MessageBox.Show("تاريخ الترقية اقل من تاريخ اليوم ... ", "تنبيه");
                            }



                            db.SaveChanges();


                            #endregion

                        }
                        isAllSelected = false;
                        OnPropertyChanged("IsAllSelected");
                        LoadAll();
                        OnPropertyChanged("Results");
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        private void SearchResult(string p)
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
                ToExcelClass.ExportEntry((DataGrid)gridExcel);
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

        #region INoitify
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
