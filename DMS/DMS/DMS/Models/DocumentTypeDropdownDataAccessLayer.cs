using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DMS.Models
{
    public class DocumentTypeDropdownDataAccessLayer
    {
        public IEnumerable<DocumentTypeDropdown> GetDocumentTypeDropdownList()
        {
            List<DocumentTypeDropdown> lstDocumentTypeDropdown = new List<DocumentTypeDropdown>();

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocTypeListDD", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DocumentTypeDropdown documentTypeDropdown = new DocumentTypeDropdown();

                    documentTypeDropdown.DocumentTypeID = Convert.ToInt32(rdr["DocumentTypeID"]);
                    documentTypeDropdown.DocumentType = rdr["DocumentType"].ToString();
                    documentTypeDropdown.DocumentTypeDesc = rdr["DocumentTypeDesc"].ToString();
                    documentTypeDropdown.DocumentDropdownText = rdr["DocumentType"].ToString() + " - " + rdr["DocumentTypeDesc"].ToString();

                    lstDocumentTypeDropdown.Add(documentTypeDropdown);
                }
                con.Close();

            }
            return lstDocumentTypeDropdown;
        }


    }
}