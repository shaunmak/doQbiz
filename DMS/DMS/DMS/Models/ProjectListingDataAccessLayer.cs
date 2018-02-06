using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DMS.Models
{
    public class ProjectListingDataAccessLayer
    {
        public IEnumerable<ProjectListing> getProjectListingAll() {

            List<ProjectListing> lstprojListing = new List<ProjectListing>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spProjectDocListAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectListing projListing = new ProjectListing();

                    projListing.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    projListing.ProjectNo = rdr["ProjectNo"].ToString();
                    projListing.DocumentType = rdr["DocumentType"].ToString();
                    projListing.DocumentID = Convert.ToInt32(rdr["DocumentID"]);
                    projListing.DocFieldTag = rdr["DocFieldTag"].ToString();
                    projListing.DocumentNo = rdr["DocumentNo"].ToString();
                    projListing.DocumentTitle = rdr["DocumentTitle"].ToString();
                    projListing.CheckOutBy = rdr["CheckoutBy"].ToString();
                    projListing.DocumentStatus = rdr["DocumentStatus"].ToString();
                    projListing.CurrentRev = rdr["CurrentRev"].ToString();
                    projListing.Originator = rdr["Originator"].ToString();

                    lstprojListing.Add(projListing);

                }
                con.Close();

            }
            return lstprojListing;

        }

        public IEnumerable<ProjectListing> getProjectListing(int intProjectID)
        {

            List<ProjectListing> lstprojListing = new List<ProjectListing>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spProjectDocListProjectID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int).Value = intProjectID;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProjectListing projListing = new ProjectListing();

                    projListing.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    projListing.ProjectNo = rdr["ProjectNo"].ToString();
                    projListing.DocumentType = rdr["DocumentType"].ToString();
                    projListing.DocumentID = Convert.ToInt32(rdr["DocumentID"]);
                    projListing.DocFieldTag = rdr["DocFieldTag"].ToString();
                    projListing.DocumentNo = rdr["DocumentNo"].ToString();
                    projListing.DocumentTitle = rdr["DocumentTitle"].ToString();
                    projListing.CheckOutBy = rdr["CheckoutBy"].ToString();
                    projListing.DocumentStatus = rdr["DocumentStatus"].ToString();
                    projListing.CurrentRev = rdr["CurrentRev"].ToString();
                    projListing.Originator = rdr["Originator"].ToString();

                    lstprojListing.Add(projListing);

                }
                con.Close();

            }
            return lstprojListing;

        }

    }


}
