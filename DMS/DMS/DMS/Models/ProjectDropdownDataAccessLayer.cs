using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DMS.Models
{
    public class ProjectDropdownDataAccessLayer
    {
            // TO DO - this has to be obtained from the appsettings.json file.  Gavin 8th Jan 2018
            //string strConnectionString = "Data Source=Gavin-PC;Initial Catalog=DMS;Trusted_Connection=yes;MultipleActiveResultSets=true";

        public IEnumerable<ProjectDropdown> GetProjectDropdownList()
        {
            List<ProjectDropdown> lstProjectDropdown = new List<ProjectDropdown>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spProjectListDD", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectDropdown projectDropdown = new ProjectDropdown();

                    projectDropdown.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    projectDropdown.ProjectNo = rdr["ProjectNo"].ToString();

                    lstProjectDropdown.Add(projectDropdown);
                }
                con.Close();

            }
            return lstProjectDropdown;
        }


    }
}
