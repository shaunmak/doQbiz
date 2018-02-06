using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;


namespace DMS.Models
{
    public class DocUserListDataAccessLayer
    {
        public IEnumerable<DocUserList> DocUserListing()
        {
           
             List<DocUserList> lstDocUserList = new List<DocUserList>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocDataListUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = WindowsIdentity.GetCurrent().Name;
                // TO DO - Currently only configurable in the stored procedure.  May be a requirement to move it into the application at a later date - Gavin 02 Feb 2018
                cmd.Parameters.Add("@DocCount", System.Data.SqlDbType.Int).Value = null;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DocUserList docUserList = new DocUserList();

                    docUserList.DocumentID = Convert.ToInt32(rdr["DocumentID"]);
                    docUserList.DocumentNo = rdr["DocumentNo"].ToString();
                    docUserList.DocumentTitle = rdr["DocumentTitle"].ToString();
                    docUserList.DocFileName = rdr["DocFileName"].ToString();
                    docUserList.CurrentRev = rdr["CurrentRev"].ToString();
                    docUserList.DocumentStatus = rdr["DocumentStatus"].ToString();
                    docUserList.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    docUserList.ProjectNo = rdr["ProjectNo"].ToString();
                    docUserList.LastUpdateBy = rdr["LastUpdateBy"].ToString();
                    docUserList.LastUpdate = Convert.ToDateTime(rdr["LastUpdate"]);



                    lstDocUserList.Add(docUserList);


                }
                con.Close();

            }
            return lstDocUserList;

        }


    }
}
