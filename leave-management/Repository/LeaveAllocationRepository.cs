﻿using leave_management.Contracts;
using leave_management.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
           return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.LeaveAllocations.Include(x=>x.LeaveType).ToList();
       
        }

        public bool IsExists(int id)
        {
            return _db.LeaveAllocations.Any(x => x.Id == id);
        }
        public LeaveAllocation FindById(int id)
        {
           var leaveAllocation =  _db.LeaveAllocations.
                Include(x=>x.LeaveType).
                Include(x=>x.Employee).
                FirstOrDefault(x=>x.Id == id);
            return leaveAllocation;
        }

        public bool Save()
        {
           return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }

        public bool CheckEmployeeHasLeaveAssigned(int leaveTypeId, string empId)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(x => x.LeaveTypeId == leaveTypeId && x.EmployeeId == empId && x.Period == period).Any();
        }
    }
}
