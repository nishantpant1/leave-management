using leave_management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveAllocationVM
    {
      
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Employees { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public int Period { get; set; }
    }

    public class CreateAllocationVM
    {
        public int NumberofDays { get; set; }

        public List<LeaveTypeVM> LeaveTypes { get; set; } 
    }

    public class EditAllocationVM
    {
        public int Id { get; set; }
        public int NumberofDays { get; set; }

        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }

    }

    public class ViewAllocationVM
    {
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> LeaveAllocation { get; set; }
    }
}
