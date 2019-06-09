using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalMerchBuild.Models
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }

        [Display(Name = "Section Height")]
        [Required, Range(20, 200)]
        public double Height { get; set; }

        [Display(Name = "Section Width")]
        [Required, Range(30, 1200)]
        public double Width { get; set; }

        [Display(Name = "Section Depth")]
        [Required, Range(15, 60)]
        public double Depth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }


        public virtual ICollection<Bay> Bays { get; set; }

    }
}