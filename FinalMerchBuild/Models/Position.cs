using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{
    public class Position
    {

            [Key]
            public int PositionID { get; set; }

            [Required]
            [Range(0, 9)]
            public int Shelf { get; set; }

            [Required]
            public string UPC { get; set; }

            [Required]
            public double XLocation { get; set; }

            public virtual ICollection<Fixture> Fixtures { get; set; }

        
    }
}