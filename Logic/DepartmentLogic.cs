using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class DepartmentLogic
    {
        private DepartmentContext _departmentContext = new DepartmentContext();

        public List<Department> GetAllDepartments()
        {
            return _departmentContext.GetAllDepartments();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _departmentContext.GetDepartmentById(departmentId);
        }
    }
}
