using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace IContext
{
    public interface IDepartmentContext
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
    }
}
