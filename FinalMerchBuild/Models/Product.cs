using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string UPC { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string Size { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Depth { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Name + ", " + Size;
            }
        }


        public virtual Position Position { get; set; }
    }
}