using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class LeaveAllocationController : Controller
    {
       private readonly ILeaveTypeRepository _leaveTypeRepo;
       private readonly IMapper _mapper;
       private readonly ILeaveAllocationRepository _leaveAllocationRepo;
       private readonly UserManager<Employee> _userManager;

       public LeaveAllocationController(ILeaveTypeRepository leaveTypeRepo,
                                        IMapper mapper,
                                        ILeaveAllocationRepository leaveAllocaitonRepo,
                                        UserManager<Employee> userManager)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _leaveAllocationRepo = leaveAllocaitonRepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var leaveTypes = _leaveTypeRepo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        public IActionResult AllocateLeave(int id)
        {
            var leaveType = _leaveTypeRepo.FindById(id);
            var employeeList = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employeeList)
            {
                //Check if employee is not already assigned with the leaves.
                //Number of Days should be default of each leave type
                //Period for which leave has been allocated should be defined.
                if (_leaveAllocationRepo.CheckEmployeeHasLeaveAssigned(id, emp.Id) == true)
                    continue;
                var allocation = new LeaveAllocationVM
                {
                    LeaveTypeId = id,
                    EmployeeId = emp.Id,
                    NumberOfDays = 10,
                    DateCreated = DateTime.Now
                };
                var leaveAllocationEntity = _mapper.Map<LeaveAllocation>(allocation);
                _leaveAllocationRepo.Create(leaveAllocationEntity);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employeeList = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(employeeList);
            return View(model);
        }

        public ActionResult Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>( _userManager.FindByIdAsync(id).Result);
            var leaveAllocated = _mapper.Map<List<LeaveAllocationVM>>(_leaveAllocationRepo.FindAll().Where(x => x.EmployeeId == id && x.Period == DateTime.Now.Year));
            var model = new ViewAllocationVM
            {
                Employee = employee,
                EmployeeId = employee.Id,
                LeaveAllocation = leaveAllocated

            };
            //Bind the View with ViewAllocationVM
            //EmployeeId, EmployeeName, LeaveAllocationVMList
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var leaveAllocationDetails = _leaveAllocationRepo.FindById(id);
            var model = _mapper.Map<EditAllocationVM>(leaveAllocationDetails);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditAllocationVM entity)
        {
           if(ModelState.IsValid == false)
            {
                return View(entity);
            }

            var record = _leaveAllocationRepo.FindById(entity.Id);
            record.NumberOfDays = entity.NumberofDays;
            
           if( _leaveAllocationRepo.Update(record) == false)
            {
                ModelState.AddModelError("", "Error while updating the record");
                return View(entity);
            }
            return RedirectToAction(nameof(Details), new { Id = entity.EmployeeId });
        }
    }
}
