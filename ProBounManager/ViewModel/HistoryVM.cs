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
    class HistoryVM : INotifyPropertyChanged
    {
        #region Variables
        List<PromoHistory> promoHistories;
        Emp selectedEmp;
        string promoSearch;
        PromoHistory promoHistory;
        List<PromoHistory> filterPromos;
        PromoHistory selected1;
        //string txtSearch;
        List<Emp> emps;
        List<Result> results;

        //----------------------------------//
        List<BouncHistory> bouncHistories;
        Emp selectedBounc;
        string bouncSearch;
        BouncHistory bouncHistory;
        List<BouncHistory> filterBounces;
        List<Rank> filterRanks;
        Rank selectedFilterRank;
        BouncHistory selected2;
        #endregion

        #region Properties
        public List<PromoHistory> PromoHistories { get { return promoHistories; } set { if (promoHistories != value) { promoHistories = value; OnPropertyChanged("PromoHistories"); } } }
        public List<BouncHistory> BouncHistories { get { return bouncHistories; } set { if (bouncHistories != value) { bouncHistories = value; OnPropertyChanged("BouncHistories"); } } }
        public BouncHistory BouncHistory { get { return bouncHistory; } set { if (bouncHistory != value) { bouncHistory = value; OnPropertyChanged("BouncHistory"); } } }
        public List<Emp> Emps { get { return emps; } set { if (emps != value) { emps = value; OnPropertyChanged("Emps"); } } }
        public List<Result> Results { get { return results; } set { if (results != value) { results = value; OnPropertyChanged("Results"); } } }

        public List<PromoHistory> FilterPromos { get { return filterPromos; } set { if (filterPromos != value) { filterPromos = value; OnPropertyChanged("FilterPromos"); } } }
        public Emp SelectedEmp { get { return selectedEmp; } set { if (selectedEmp != value) { selectedEmp = value; OnPropertyChanged("SelectedEmp"); OnSelectedEmp(); } } }
        public PromoHistory Selected1 { get { return selected1; } set { if (selected1 != value) { selected1 = value; OnPropertyChanged("Selected1"); } } }
        public PromoHistory PromoHistory { get { return promoHistory; } set { if (promoHistory != value) { promoHistory = value; OnPropertyChanged("PromoHistory"); } } }
        public string PromoSearch { get { return promoSearch; } set { if (promoSearch != value) { promoSearch = value; SearchPromo(promoSearch);  OnPropertyChanged("PromoSearch"); } } }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public List<BouncHistory> FilterBounces { get { return filterBounces; } set { if (filterBounces != value) { filterBounces = value; OnPropertyChanged("FilterBounces"); } } }
        public Emp SelectedBounc { get { return selectedBounc; } set { if (selectedBounc != value) { selectedBounc = value; OnPropertyChanged("SelectedBounc"); OnSelectedBounc(); } } }
        public BouncHistory Selected2 { get { return selected2; } set { if (selected2 != value) { selected2 = value; OnPropertyChanged("Selected2"); } } }
        public List<Rank> FilterRanks { get { return filterRanks; } set { if (filterRanks != value) { filterRanks = value; OnPropertyChanged("FilterRanks"); } } }
        public Rank SelectedFilterRank { get { return selectedFilterRank; } set { if (selectedFilterRank != value) { selectedFilterRank = value; OnPropertyChanged("SelectedFilterRank"); OnSelectedFilterRank(); } } }
        public string BouncSearch { get { return bouncSearch; } set { if (bouncSearch != value) { bouncSearch = value; OnPropertyChanged("BouncSearch"); SearchBounc(bouncSearch); } } }

        public DelegateCommand<object> ExportCommandPromo { get; private set; }
        public DelegateCommand<object> ExportCommandBounc { get; private set; }
        public TheCommands DelResetPromo { get; set; }
            public TheCommands DelResetBounc { get; set; }
            public TheCommands EditPromoHistory { get; set; }
            public TheCommands EditBouncHistory { get; set; }
        #endregion

        #region CTOR
        public HistoryVM()
        {
            this.ExportCommandPromo = new DelegateCommand<object>(this.ExportPromo);
            this.ExportCommandBounc = new DelegateCommand<object>(this.ExportBounc);

            //DelResetPromo = new TheCommands(OnDelResetPromo);
            //DelResetBounc = new TheCommands(OnDelResetBounc);
            //EditPromoHistory = new TheCommands(OnEditPromo);
            //EditBouncHistory = new TheCommands(OnEditBounc);

        }
        #endregion

        #region VoidOfPromoHistory
        void SearchPromo(string p)
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    emps = db.Emps.Where(s => s.EmpName.Contains(p)).ToList();
                    OnPropertyChanged("Emps");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        

        }
        void OnSelectedEmp()
        {
            try
            {
                if (selectedEmp != null)
                {
                    using (DBEmp db = new DBEmp())
                    {

                        filterPromos = db.PromoHistories.Include(z => z.Rank).Include(c => c.Emp).Where(x => x.EmpId == selectedEmp.EmpId).ToList();
                    }
                    OnPropertyChanged("FilterPromos");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


        }

        private void ExportPromo(object gridExcel)
        {
            if (gridExcel != null)
            {
                ToExcelClass.ExportPromoHistory((DataGrid)gridExcel);
            }
        }
        //    //void OnDelResetPromo()
        //    //{
        //    //    using (DBEmp db = new DBEmp())
        //    //    {
        //    //        if (selectedPromo != null)
        //    //        {
        //    //            var promo1 = db.PromoHistories.Where(x => x.PromoHistoryName == selectedPromo.PromoHistoryName).ToList();
        //    //            foreach (var item in promo1)
        //    //            {
        //    //                db.PromoHistories.Remove(item);
        //    //            }
        //    //            db.SaveChanges();
        //    //            MessageBox.Show("تم حذف الموظف الحالي من سجلات الترقية");
        //    //            promoHistories = new List<PromoHistory>();
        //    //            filterPromos = new List<PromoHistory>();
        //    //            OnPropertyChanged("PromoHistories");
        //    //            OnPropertyChanged("FilterPromos");
        //    //        }
        //    //        else MessageBox.Show("اختر موظف رجاءا");

        //    //    }
        //    //}
        //    void OnEditPromo()
        //    {
        //        using (DBEmp db = new DBEmp())
        //        {
        //            if (selected1 != null)
        //            {
        //                db.Update(selected1);

        //            }

        //            OnPropertyChanged("PromoHistories");
        //            OnPropertyChanged("FilterPromos");
        //        }
        //    }
        #endregion

        #region VoidOfBouncHistory
        void SearchBounc(string p)
        {
            try
            {
                using (DBEmp db = new DBEmp())
                {
                    emps = db.Emps.Include(z => z.PromoHistories).Where(s => s.EmpName.Contains(p)).ToList();
                    OnPropertyChanged("Emps");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        



        }
        void OnSelectedBounc()
        {
            try
            {
                if (selectedBounc != null)
                {
                    using (DBEmp db = new DBEmp())
                    {

                        filterRanks = db.PromoHistories.Include(z => z.Emp).Include(c => c.Rank).Where(x => x.EmpId == selectedBounc.EmpId).Select(z => z.Rank).ToList();
                    }
                    OnPropertyChanged("FilterRanks");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
       


        }
        void OnSelectedFilterRank()
        {
            try
            {
                if (selectedBounc != null && selectedFilterRank != null)
                {
                    using (DBEmp db = new DBEmp())
                    {
                        filterBounces = new List<BouncHistory>();
                        filterBounces = db.BouncHistories.Include(z => z.Stage).Include(v => v.PromoHistory.Rank).Include(b => b.PromoHistory.Emp).Where(x => x.PromoHistory.RankId == selectedFilterRank.RankId && x.PromoHistory.Emp.EmpName == selectedBounc.EmpName).ToList();

                    }
                    OnPropertyChanged("FilterBounces");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


        }

        private void ExportBounc(object gridExcel)
        {
            if (gridExcel != null)
            {
                ToExcelClass.ExportBouncHistory((DataGrid)gridExcel);
            }
        }
        #endregion


        #region INotify
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
