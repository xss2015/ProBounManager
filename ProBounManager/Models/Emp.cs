using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBounManager.Models
{
  public  class Emp : INotifyPropertyChanged
    {
        #region Variables

        int privilageYearId, privilageMonthId, penaltyId;
        DateTime empDateHiring;
        bool empCheckSelect;
        #endregion

        public int EmpId { get; set; }
        [NotMapped]
        public bool EmpCheckSelect { get { return empCheckSelect; } set { if (empCheckSelect != value) { empCheckSelect = value;OnPropertyChanged("EmpCheckSelect"); } } }
        public string EmpName { get; set; }
        [Column(TypeName = "date")]
        public DateTime EmpDateHiring { get { return empDateHiring; } set { empDateHiring = value; } }
        public int EmpNO { get; set; } //الرقم الاحصائي

        public int PrivilageYearId { get { return privilageYearId; } set { if (privilageYearId != value) { privilageYearId = value;OnPropertyChanged("PrivilageYearId"); } } }
        [ForeignKey(nameof(PrivilageYearId))]
        public PrivilageYear PrivilageYear { get; set; }
        public int PrivilageMonthId { get { return privilageMonthId; } set { if (privilageMonthId != value) { privilageMonthId = value; OnPropertyChanged("PrivilageMonthId"); } } }
        [ForeignKey(nameof(PrivilageMonthId))]
        public PrivilageMonth PrivilageMonth { get; set; }
        public int PenaltyId { get { return penaltyId; } set { if (penaltyId != value) { penaltyId = value; OnPropertyChanged("PenaltyId"); } } }
        [ForeignKey(nameof(PenaltyId))]
        public Penalty Penalty { get; set; }
        public string EmpNote { get; set; }
        public List<PromoHistory> PromoHistories { get; set; }
       
       
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
