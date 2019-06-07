using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{
    public class Fixture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int FixtureID { get; set; }

        [Required]
        public int SectionID { get; set; }

        [Required]
        public int PositionID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public virtual Section Section { get; set; }
        public virtual Position Position { get; set; }

    }
}