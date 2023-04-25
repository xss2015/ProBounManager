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
   public class PromoHistory : INotifyPropertyChanged
    {
        int rankId;
     
        [Key]
        public int PromoId { get; set; }

        public int RankId { get { return rankId; } set { if (rankId != value) { rankId = value; OnPropertyChanged("RankId"); } } }
        [ForeignKey(nameof(RankId))]
        public Rank Rank { get; set; }

       
        [Column(TypeName = "date")]
        public DateTime PromoCrntRankDate { get; set; }
        public string PromoCrntCareer { get; set; }
        public string PromoNextRank { get; set; }
        [Column(TypeName = "date")]
        public DateTime PromoNextRankDate { get; set; }
        public string PromoNextCareer { get; set; }
        public int EmpId { get; set; }
        [ForeignKey(nameof(EmpId))]
        public Emp Emp { get; set; }
        public List<BouncHistory> BouncHistories { get; set; }

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
