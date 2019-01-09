using System;
using System.Collections.Generic;
using System.Text;
using Data;
using IContext;
using Models;

namespace Repo
{
    public class DepartmentRepo
    {
        private IDepartmentContext iContext;

        public DepartmentRepo(Context contextType)
        {
            if (contextType == Context.Mssql)
            {
                iContext = new DepartmentSqlContext();
            }
            else if (contextType == Context.Memory)
            {
                //iContext = MemoryContext;
            }
        }

        public List<Department> GetAllDepartments()
        {
            return iContext.GetAllDepartments();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return iContext.GetDepartmentById(departmentId);
        }
    }
}
