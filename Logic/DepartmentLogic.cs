using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repo;

namespace Logic
{
    public class DepartmentLogic
    {
        private DepartmentRepo _departmentRepo = new DepartmentRepo(Context.Mssql);

        public List<Department> GetAllDepartments()
        {
            return _departmentRepo.GetAllDepartments();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _departmentRepo.GetDepartmentById(departmentId);
        }
    }
}
