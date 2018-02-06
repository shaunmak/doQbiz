using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DMS.Models
{
    public class ProjectDataAccessLayer
    {
        public Project GetProjectData (int intProjectID)
        {

            Project project = new Project();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spProjectListProjectID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int).Value = intProjectID;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    project.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    project.ProjectNo = rdr["ProjectNo"].ToString();
                    project.ProjectDesc = rdr["ProjectDesc"].ToString();
                    project.ProjectManager = rdr["ProjectManager"].ToString();
                    project.ProjectStatusID = Convert.ToInt32(rdr["ProjectStatusID"]);
                    project.ProjectStatusDesc = rdr["ProjectStatusDesc"].ToString();
                    project.FileLocation = rdr["FileLocation"].ToString();
                    project.URLFileLocation = rdr["URLFileLocation"].ToString();
                    project.IncludeInDD = Convert.ToBoolean(rdr["IncludeInDD"]);
                    project.IsDeleted = Convert.ToBoolean(rdr["IsDeleted"]);
                    project.LastUpdateBy = rdr["LastUpdateBy"].ToString();

                    if (rdr["LastUpdate"] != DBNull.Value)
                    {
                        project.LastUpdate = Convert.ToDateTime(rdr["LastUpdate"]);
                    }

                    project.LastUpdateBy = rdr["CreatedBy"].ToString();

                    if (rdr["CreateDate"] != DBNull.Value)
                    {
                        project.LastUpdate = Convert.ToDateTime(rdr["CreateDate"]);
                    }
                }
                con.Close();

            }
            return project;
        }

        public string GetFileLocation (int intProjectID)
        {
            string strFileLocation = "";

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spProjectFileLocationProjectID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int).Value = intProjectID;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    strFileLocation = rdr["FileLocation"].ToString();
                }
                con.Close();

            }
            return strFileLocation;


        }

    }
}
