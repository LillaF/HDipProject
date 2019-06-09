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
        [Key]
        public int PositionID { get; set; }

        [Required]
        public int SectionID { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Section Name")]
        //public string SectionName { get; set; }

        //[Required]
        //public int BayID { get; set; }

        [Display(Name = "Bay Name")]
        [Required]
        //[RegularExpression(@"[a-zA-L]+$", ErrorMessage = "Only letters A-L")]
        public BayName? BayName { get; set; }

        [Required]
        [Range(0, 9)]
        public int Shelf { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string Size { get; set; }
        [Required]
        public string UPC { get; set; }

        [Required]
        public double XLocation { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual Bay Bay { get; set; }
        public virtual Section Section { get; set; }
    }
}