﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyProject.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(225)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; }

        public byte GenreId { get; set; }

        //[Required]
        public DateTime ReleaseDate { get; set; }

        //[Required]
        public DateTime DateAdded { get; set; }

        //[Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
    }
}