using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DMS.Models
{
    public class DocumentStatusDropdownDataAccessLayer
    {

        public IEnumerable<DocumentStatusDropdown> GetDocumentStatusDropdownList()
        {
            List<DocumentStatusDropdown> lstDocumentStatusDropdown = new List<DocumentStatusDropdown>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocStatusListDD", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DocumentStatusDropdown documentStatusDropdown = new DocumentStatusDropdown();

                    documentStatusDropdown.DocumentStatusID = Convert.ToInt32(rdr["DocumentStatusID"]);
                    documentStatusDropdown.StatusDesc = rdr["StatusDesc"].ToString();

                    lstDocumentStatusDropdown.Add(documentStatusDropdown);
                }
                con.Close();

            }
            return lstDocumentStatusDropdown;
        }
    }
}
