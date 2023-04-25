using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBounManager.Models
{
    
   public class BouncHistory : INotifyPropertyChanged
    {

        DateTime bounCrntStageDate;
        [Key]
        public int BouncHistoryId { get; set; }
        public int StageId { get; set; }
        [ForeignKey(nameof(StageId))]
        public Stage Stage { get; set; }

        [Column(TypeName = "date")]
        public DateTime BouncCrntStageDate { get { return bounCrntStageDate; } set { if (bounCrntStageDate != value) { bounCrntStageDate = value;OnPropertyChanged("BouncCrntStageDate"); } } }
        public string BouncNextStage { get; set; }
        [Column(TypeName = "date")]
        public DateTime BouncNextStageDate { get; set; }
        public int PromoId { get; set; }
        [ForeignKey(nameof(PromoId))]
        public PromoHistory PromoHistory { get; set; }

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
