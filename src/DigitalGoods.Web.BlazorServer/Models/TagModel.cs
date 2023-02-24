﻿using DigitalGoods.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class TagModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
    }
}
