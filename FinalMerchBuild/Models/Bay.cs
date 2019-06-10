using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{

    public class Bay
    {
        [Required]
        public int SectionID { get; set; }

        [Key]
        public int BayID { get; set; }

        [Range(1, 9)]
        [Display(Name = "Bay Name")]
        [Required]
        public int BayName { get; set; }

        [Required]
        [Range(30, 200)]
        public double BayWidth { get; set; }

        [Required]
        [Range(0, 1180)]
        public double XLocation { get; set; }
        [Required]
        [Range(0, 190)]
        public double YLocation { get; set; }


        public virtual Section Section { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}