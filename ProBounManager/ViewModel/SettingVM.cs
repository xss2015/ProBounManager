using ProBounManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBounManager.ViewModel
{
  public  class SettingVM : INotifyPropertyChanged
    {

        public SettingVM()
        {
            loadRank();
            loadStage();
            loadYear();
            loadMonths();
            loadPenalty();
            loadPwd();

            rank = new Rank();
            stage = new Stage();
            privilageYear = new PrivilageYear();
            privilageMonth = new PrivilageMonth();
            penalty = new Penalty();
            PWD = new PWD();

            AddRankCommand = new TheCommands(OnAddRank);
            AddStageCommand = new TheCommands(OnAddStage);
            AddYearCommand = new TheCommands(OnAddYear);
            AddMonthCommand = new TheCommands(OnAddMonth);
            AddPenaltyCommand = new TheCommands(OnAddPenalty);
            AddPwdCommand = new TheCommands(OnAddPwd);

            EditRankCommand = new TheCommands(OnEditRank);
            EditStageCommand = new TheCommands(OnEditStage);
            EditYearCommand = new TheCommands(OnEditYear);
            EditMonthCommand = new TheCommands(OnEditMonth);
            EditPenalty = new TheCommands(OnEditPenalty);
            EditPwd = new TheCommands(OnEditPwd);

            DelRankCommand = new TheCommands(OnDelRank);
            DelStageCommand = new TheCommands(OnDelStage);
            DelYearCommand = new TheCommands(OnDelYear);
            DelMonthCommand = new TheCommands(OnDelMonth);
            DelPenaltyCommand = new TheCommands(OnDelPenalty);
            DelPwdCommand = new TheCommands(OnDelPwd);
        }
        #region Variables

        List<Rank> ranks;
        List<Stage> stages;
        List<PrivilageYear> privilageYears;
        List<PrivilageMonth> privilageMonths;
        List<Penalty> penalties;
        List<PWD> pWDs;
        Rank rank;
        Stage stage;
        PrivilageYear privilageYear;
        PrivilageMonth privilageMonth;
        Penalty penalty;
        PWD pWD;
        Rank selectedRank;
        Stage selectedStage;
        PrivilageYear selectedPrivilageYear;
        PrivilageMonth selectedPrivilageMonth;
        Penalty selectedPenalty;
        PWD selectedPWD;

        #endregion

        #region Properties

        public List<PrivilageMonth> PrivilageMonths { get { return privilageMonths; } set { if (privilageMonths != value) { privilageMonths = value; OnPropertyChanged("PrivilageMonths"); } } }
        public List<PrivilageYear> PrivilageYears { get { return privilageYears; } set { if (privilageYears != value) { privilageYears = value; OnPropertyChanged("PrivilageYears"); } } }
        public List<Penalty> Penalties { get { return penalties; } set { if (penalties != value) { penalties = value; OnPropertyChanged("Penalties"); } } }
        public List<Rank> Ranks { get { return ranks; } set { if (ranks != value) { ranks = value; OnPropertyChanged("Ranks"); } } }
        public List<Stage> Stages { get { return stages; } set { if (stages != value) { stages = value; OnPropertyChanged("Stages"); } } }
        public List<PWD> PWDs { get { return pWDs; } set { if (pWDs != value) { pWDs = value; OnPropertyChanged("PWDs"); } } }
        public Rank Rank { get { return rank; } set { if (rank != value) { rank = value; OnPropertyChanged("Rank"); } } }
        public Stage Stage { get { return stage; } set { if (stage != value) { stage = value; OnPropertyChanged("Stage"); } } }
        public PrivilageYear PrivilageYear { get { return privilageYear; } set { if (privilageYear != value) { privilageYear = value; OnPropertyChanged("PrivilageYear"); } } }
        public PrivilageMonth PrivilageMonth { get { return privilageMonth; } set { if (privilageMonth != value) { privilageMonth = value; OnPropertyChanged("PrivilageMonth"); } } }
        public Penalty Penalty { get { return penalty; } set { if (penalty != value) { penalty = value; OnPropertyChanged("Penalty"); } } }
        public PWD PWD { get { return pWD; } set { if (pWD != value) { pWD = value; OnPropertyChanged("PWD"); } } }
        public Rank SelectedRank { get { return selectedRank; } set { if (selectedRank != value) { selectedRank = value; OnPropertyChanged("SelectedRank"); } } }
        public Stage SelectedStage { get { return selectedStage; } set { if (selectedStage != value) { selectedStage = value; OnPropertyChanged("SelectedStage"); } } }
        public PrivilageYear SelectedPrivilageYear { get { return selectedPrivilageYear; } set { if (selectedPrivilageYear != value) { selectedPrivilageYear = value; OnPropertyChanged("SelectedPrivilageYear"); } } }
        public PrivilageMonth SelectedPrivilageMonth { get { return selectedPrivilageMonth; } set { if (selectedPrivilageMonth != value) { selectedPrivilageMonth = value; OnPropertyChanged("SelectedPrivilageMonth"); } } }
        public Penalty SelectedPenalty { get { return selectedPenalty; } set { if (selectedPenalty != value) { selectedPenalty = value; OnPropertyChanged("SelectedPenalty"); } } }
        public PWD SelectedPWD { get { return selectedPWD; } set { if (selectedPWD != value) { selectedPWD = value; OnPropertyChanged("SelectedPWD"); } } }

        public TheCommands AddRankCommand { get; set; }
        public TheCommands AddStageCommand { get; set; }
        public TheCommands AddYearCommand { get; set; }
        public TheCommands AddMonthCommand { get; set; }
        public TheCommands AddPenaltyCommand { get; set; }
        public TheCommands AddPwdCommand { get; set; }

        public TheCommands DelRankCommand { get; set; }
        public TheCommands DelStageCommand { get; set; }
        public TheCommands DelYearCommand { get; set; }
        public TheCommands DelMonthCommand { get; set; }
        public TheCommands DelPenaltyCommand { get; set; }
        public TheCommands DelPwdCommand { get; set; }

        public TheCommands EditRankCommand { get; set; }
        public TheCommands EditStageCommand { get; set; }
        public TheCommands EditYearCommand { get; set; }
        public TheCommands EditMonthCommand { get; set; }
        public TheCommands EditPenalty { get; set; }
        public TheCommands EditPwd { get; set; }

        #endregion

        #region Voids

        void loadRank()
        {
            using (DBEmp db = new DBEmp())
            {
                ranks = db.Ranks.ToList();
            }
            OnPropertyChanged("Ranks");
        }

        void loadStage()
        {
            using (DBEmp db = new DBEmp())
            {
                stages = db.Stages.ToList();
            }
            OnPropertyChanged("Stages");
        }
        void loadYear()
        {
            using (DBEmp db = new DBEmp())
            {
                privilageYears = db.PrivilageYears.ToList();
            }
            OnPropertyChanged("PrivilageYears");
        }
        void loadMonths()
        {
            using (DBEmp db = new DBEmp())
            {
                privilageMonths = db.PrivilageMonths.ToList();
            }
            OnPropertyChanged("PrivilageMonths");
        }
        void loadPenalty()
        {
            using (DBEmp db = new DBEmp())
            {
                penalties = db.Penalties.ToList();
            }
            OnPropertyChanged("Stages");
        }
        void loadPwd()
        {
            using (DBEmp db = new DBEmp())
            {
                pWDs = db.PWDs.ToList();
            }
            OnPropertyChanged("PWDs");
        }

        void OnAddRank()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Ranks.Add(rank);
                db.SaveChanges();
            }

            loadRank();
        }
        void OnAddStage()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Stages.Add(stage);
                db.SaveChanges();
            }

            loadStage();
        }
        void OnAddYear()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageYears.Add(privilageYear);
                db.SaveChanges();
            }

            loadYear();
        }
        void OnAddMonth()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageMonths.Add(privilageMonth);
                db.SaveChanges();
            }

            loadMonths();
        }
        void OnAddPenalty()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Penalties.Add(penalty);
                db.SaveChanges();
            }

            loadPenalty();
        }
        void OnAddPwd()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PWDs.Add(pWD);
                db.SaveChanges();
            }

            loadPwd();
        }

        void OnEditRank()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Ranks.Update(selectedRank);
                db.SaveChanges();
            }
            loadRank();
        }
        void OnEditStage()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Stages.Update(selectedStage);
                db.SaveChanges();
            }
            loadStage();
        }
        void OnEditYear()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageYears.Update(selectedPrivilageYear);
                db.SaveChanges();
            }
            loadYear();
        }
        void OnEditMonth()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageMonths.Update(selectedPrivilageMonth);
                db.SaveChanges();
            }
            loadMonths();
        }
        void OnEditPenalty()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Penalties.Update(selectedPenalty);
                db.SaveChanges();
            }
            loadPenalty();
        }
        void OnEditPwd()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PWDs.Update(selectedPWD);
                db.SaveChanges();
            }
            loadPwd();
        }
        void OnDelRank()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Ranks.Remove(selectedRank);
                db.SaveChanges();
            }

            loadRank();
        }
        void OnDelStage()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Stages.Remove(selectedStage);
                db.SaveChanges();
            }

            loadStage();
        }
        void OnDelYear()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageYears.Remove(selectedPrivilageYear);
                db.SaveChanges();
            }

            loadYear();
        }
        void OnDelMonth()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PrivilageMonths.Remove(selectedPrivilageMonth);
                db.SaveChanges();
            }

            loadMonths();
        }
        void OnDelPenalty()
        {
            using (DBEmp db = new DBEmp())
            {
                db.Penalties.Remove(selectedPenalty);
                db.SaveChanges();
            }

            loadPenalty();
        }
        void OnDelPwd()
        {
            using (DBEmp db = new DBEmp())
            {
                db.PWDs.Remove(selectedPWD);
                db.SaveChanges();
            }

            loadPwd();
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
