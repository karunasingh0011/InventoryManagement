using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryMangement.Models
{
    public class InvClass
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [Display(Name= "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Upload Image")]
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}