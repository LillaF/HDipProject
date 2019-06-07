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

        public int SectionName { get; set; }
        [Key]
        public int BayID { get; set; }

        [Display(Name = "Bay Name")]
        [Required]
        public BayName? BayName { get; set; }

        [Required]
        [Range(30, 200)]
        public double BayWidth { get; set; }



        public virtual Section Section { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}