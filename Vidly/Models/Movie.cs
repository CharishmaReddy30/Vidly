using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]

        [Display(Name ="Number In Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }

        [Required]

        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}