using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using IContext;
using Models;

namespace Data
{
    public class DepartmentSqlContext:IDepartmentContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Department";
            var departments = new List<Department>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var department = new Department()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };
                            departments.Add(department);
                        }

                        return departments;
                    }
                }
            }
        }

        public Department GetDepartmentById(int departmentId)
        {
            string query = "SELECT * FROM Department WHERE Id = @Id";
            var department = new Department();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", departmentId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            department = new Department()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };
                        }
                        return department;
                    }

                }
            }
        }
    }
}
