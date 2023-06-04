using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime date { get; set; }
        public string ChildHouse { get; set; }
        public int ChildCount { get; set; }
        public string IdUser { get; set; }
        public ICollection<MenuFood> MenuFoods { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
    }
}