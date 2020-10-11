using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
  
    public class LeaveTypeVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Date Created")]
        public DateTime DateCreated { get; set; }
        [Required]
        [DisplayName("Default No of Days")]
        [Range(1,25,ErrorMessage = "No of Days doesn't fall in Range (1,25)")]
        public int DefaultDays { get; set; }
    }
}
