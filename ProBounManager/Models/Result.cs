using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBounManager.Models
{[NotMapped]
    class Result : INotifyPropertyChanged
    {

        //bool empCheckSelect;
        //DateTime empDateHiring, promoCrntRankDate;
        //int privilageYearId, privilageMonthId, penaltyId,rankId,stageId;
        //public int  ResultId { get; set; }

        //public int EmpId { get; set; }
        //[NotMapped]
        //public bool EmpCheckSelect { get { return empCheckSelect; } set { if (empCheckSelect != value) { empCheckSelect = value; OnPropertyChanged("EmpCheckSelect"); } } }
        //public string EmpName { get; set; }
        //[Column(TypeName = "date")]
        //public DateTime EmpDateHiring { get { return empDateHiring; } set { empDateHiring = value; } }
        //public int EmpNO { get; set; } //الرقم الاحصائي

        //public int PrivilageYearId { get { return privilageYearId; } set { if (privilageYearId != value) { privilageYearId = value; OnPropertyChanged("PrivilageYearId"); } } }
        //[ForeignKey(nameof(privilageYearId))]
        //public PrivilageYear PrivilageYear { get; set; }
        //public int PrivilageMonthId { get { return privilageMonthId; } set { if (privilageMonthId != value) { privilageMonthId = value; OnPropertyChanged("PrivilageMonthId"); } } }
        //[ForeignKey(nameof(PrivilageMonthId))]
        //public PrivilageMonth PrivilageMonth { get; set; }
        //public int PenaltyId { get { return penaltyId; } set { if (penaltyId != value) { penaltyId = value; OnPropertyChanged("PenaltyId"); } } }
        //[ForeignKey(nameof(PenaltyId))]
        //public Penalty Penalty { get; set; }
        //public string EmpNote { get; set; }
        //public List<PromoHistory> PromoHistories { get; set; }
        ////-------------------
        //public int BouncHistoryId { get; set; }
        //public int StageId { get; set; }
        //[ForeignKey(nameof(StageId))]
        //public Stage Stage { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime BouncCrntStageDate { get; set; }
        //public string BouncNextStage { get; set; }
        //[Column(TypeName = "date")]
        //public DateTime BouncNextStageDate { get; set; }

        //public PromoHistory PromoHistory { get; set; }
        ////----------------
        //public int PromoId { get; set; }

        //public int RankId { get { return rankId; } set { if (rankId != value) { rankId = value; OnPropertyChanged("RankId"); } } }
        //[ForeignKey(nameof(RankId))]
        //public Rank Rank { get; set; }


        //[Column(TypeName = "date")]
        //public DateTime PromoCrntRankDate { get; set; }
        //public string PromoCrntCareer { get; set; }
        //public string PromoNextRank { get; set; }
        //[Column(TypeName = "date")]
        //public DateTime PromoNextRankDate { get; set; }
        //public string PromoNextCareer { get; set; }
        //public Emp Emp { get; set; }
        //public List<BouncHistory> BouncHistories { get; set; }
        bool checkSelected;
        [NotMapped]
        public bool CheckSelected { get { return checkSelected; } set { if (checkSelected != value) { checkSelected = value;OnPropertyChanged("CheckSelected"); } } }
        public Emp Emp { get; set; }
        public PromoHistory PromoHistory { get; set; }
        public BouncHistory BouncHistory { get; set; }
        [NotMapped]
        public int ResultMonth { get; set; }
        [NotMapped]
        public int ResultYear { get; set; }

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
