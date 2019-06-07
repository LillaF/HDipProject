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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PositionID { get; set; }

        [Required]
        public int SectionID { get; set; }

        [Display(Name = "Section Name")]
        [Required]
        public int SectionName { get; set; }

        [Display(Name = "Bay Name")]
        [Required]
        public int BayName { get; set; }

        [Required]
        [Range(0, 9)]
        public int Shelf { get; set; }

        [Required]
        public string UPC { get; set; }

        [Required]
        public double XLocation { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual Bay Bay { get; set; }

    }
}