using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalMerchBuild.Models;

namespace FinalMerchBuild.Models

{
    public class Fixture
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }

        [Required]
        public int BayID { get; set; }

        [Required]
        public int BayName { get; set; }

        [Key]
        public int FixtureID { get; set; }

        [Display(Name = "Fixture Name")]
        [Required]
        public int FixName { get; set; }

        [Required]
        public double FixHeight { get; set; }

        [Required]
        public double FixWidth { get; set; }

        [Required]
        public double FixDepth { get; set; }

        [Required]
        public double XLocation { get; set; }

        [Required]
        public double YLocation { get; set; }


        public virtual Bay Bay { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Position> Positions { get; set; }

    }
}