using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class WeekSqlContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        //public List<Week> GetAllWeeks()
        //{
        //    string query = "SELECT * From Week";
        //    var weeks = new List<Week>();

        //    using (var conn = new SqlConnection(connectionstring))
        //    {
        //        using (var cmd = new SqlCommand(query, conn))
        //        {
        //            conn.Open();
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var week = new Week()
        //                    {
        //                        Weeknumber = (int)reader["Weeknumber"],
        //                        From = (DateTime)reader["From"],
        //                        Till = (DateTime)reader["Till"]
        //                    };
        //                    weeks.Add(week);
        //                }

        //                return weeks;
        //            }
        //        }
        //    }
        //}

        //public Week GetWeekByNumber(int weeknumber)
        //{
        //    string query = "SELECT * FROM Week WHERE Weeknumber=@Weeknumber";
        //    var week = new Week();
        //    using (var conn = new SqlConnection(connectionstring))
        //    {
        //        using (var cmd = new SqlCommand(query, conn))
        //        {
        //            conn.Open();
        //            cmd.Parameters.AddWithValue("@Weeknumber", weeknumber);
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    week = new Week()
        //                    {
        //                        Weeknumber = (int)reader["Weeknumber"],
        //                        From = (DateTime)reader["From"],
        //                        Till = (DateTime)reader["Till"]
        //                    };
        //                }
        //            }

        //            return week;
        //        }
        //    }
        //}
    }
}
