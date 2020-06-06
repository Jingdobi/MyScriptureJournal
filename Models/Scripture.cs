using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesScripture.Models
{
    public class Scripture
    {
        public int ID { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }

        [Range(1, 150)]
        public int Chapter { get; set; }

        [Range(1, 176)]
        public int Verse { get; set; }
        public String Notes { get; set; }

        [Display(Name = "Added Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddDate { get; set; }

    }
}