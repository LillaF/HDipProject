using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{

    public class Position
    {
        [Required]
        public int SectionID { get; set; }

        [Key]
        public int PositionID { get; set; }

        [Range(1, 9)]
        [Display(Name = "Bay Name")]
        [Required]
        public int BayName { get; set; }

        [Required]
        [Range(1, 9)]
        public int Shelf { get; set; }

        [Required]
        public double UPC { get; set; }

        [Required]
        public double XLocation { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual Bay Bay { get; set; }
        public virtual Section Section { get; set; }
    }
}