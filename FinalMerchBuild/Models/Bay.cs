using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{
    public enum BayName
    {
        A, B, C, D, E, F, G, H, I, J, K, L
    }
    public class Bay
    {
        [Required]
        public int SectionID { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Section Name")]
        //public string SectionName { get; set; }
        [Key]
        public int BayID { get; set; }

        [Display(Name = "Bay Name")]
        [Required]
        public BayName? BayName { get; set; }

        [Required]
        [Range(30, 200)]
        public double BayWidth { get; set; }

        [Required]
        [Range(0, 180)]
        public double XLocation { get; set; }
        [Required]
        [Range(0, 190)]
        public double YLocation { get; set; }


        public virtual Section Section { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}