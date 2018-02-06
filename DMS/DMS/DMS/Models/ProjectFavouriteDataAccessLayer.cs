using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Principal;

namespace DMS.Models
{
    public class ProjectFavouriteDataAccessLayer
    {

        public IEnumerable<ProjectFavourite> ProjectFavouriteUserListing()
        {

            List<ProjectFavourite> lstProjectFavouriteList = new List<ProjectFavourite>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spUserProjectFavouriteListing", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = WindowsIdentity.GetCurrent().Name;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectFavourite projectFavouriteList = new ProjectFavourite();

                    projectFavouriteList.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    projectFavouriteList.ProjectNo = rdr["ProjectNo"].ToString();
                    projectFavouriteList.ProjectDesc = rdr["ProjectDesc"].ToString();
                    projectFavouriteList.ProjectManager = rdr["ProjectManager"].ToString();
                    projectFavouriteList.ProjectStatusDesc = rdr["ProjectStatusDesc"].ToString();

                    lstProjectFavouriteList.Add(projectFavouriteList);


                }
                con.Close();

            }
            return lstProjectFavouriteList;

        }
    }
}
