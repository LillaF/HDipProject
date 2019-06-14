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

        [Range(1, 15)]
        [Display(Name = "Bay Name")]
        [Required]
        public int BayName { get; set; }

        [Required]
        public double BayHeight { get; set; }

        [Required]
        public double BayWidth { get; set; }

        [Required]
        public double BayDepth { get; set; }

        [Required]
        public double XLocation { get; set; }
        [Required]
        public double YLocation { get; set; }

        [Display(Name = "Number of Fixtures")]
        public int NumFix { get; set; }


        public virtual Section Section { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}