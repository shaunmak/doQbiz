using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;


namespace DMS.Models
{
    public class DocumentDataAccessLayer
    {

        public Document GetDocumentData (int intDocumentID) {

            Document document = new Document();
            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocDataListDocumentID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@DocumentID", System.Data.SqlDbType.Int).Value = intDocumentID;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    document.DocumentID = Convert.ToInt32(rdr["DocumentID"]);
                    document.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    document.ProjectNo = rdr["ProjectNo"].ToString();
                    document.DocumentTypeID = Convert.ToInt32(rdr["DocumentTypeID"]);
                    document.DocumentType = rdr["DocumentType"].ToString();
                    document.DocumentTypeDesc = rdr["DocumentTypeDesc"].ToString();
                    document.DocumentNo = rdr["DocumentNo"].ToString();
                    document.DocumentTitle = rdr["DocumentTitle"].ToString();
                    document.CurrentRev = rdr["CurrentRev"].ToString();
                    document.DocumentStatusID = Convert.ToInt32(rdr["DocumentStatusID"]);
                    document.DocumentStatus = rdr["DocumentStatus"].ToString();
                    document.DocFileLocation = rdr["DocFileLocation"].ToString();
                    document.DocFileName = rdr["DocFileName"].ToString();
                    document.AttachmentFileName = rdr["AttachmentFileName"].ToString();
                    document.ReviewedBy = rdr["ReviewedBy"].ToString();

                    // TO DO - workout a short hand way of doing this that doesn't rely on the If statements. Gavin 10-Jan-2018
                    if (rdr["ReviewDate"] != DBNull.Value) {
                        document.ReviewDate = Convert.ToDateTime(rdr["ReviewDate"]);
                    }
                    document.SignedOffBy = rdr["SignedOffBy"].ToString();
                    if (rdr["SignoffDate"] != DBNull.Value) {
                        document.SignoffDate = Convert.ToDateTime(rdr["SignOffDate"]);
                    }
                    if (rdr["IsCheckedOut"] != DBNull.Value) {
                        document.IsCheckedOut = Convert.ToBoolean(rdr["IsCheckedOut"]);
                    }
                    document.CheckOutBy = rdr["CheckOutBy"].ToString();
                    if (rdr["CheckOutDate"] != DBNull.Value)
                    {
                        document.CheckOutDate = Convert.ToDateTime(rdr["CheckOutDate"]);
                    }
                    document.IsDeleted = Convert.ToBoolean(rdr["IsDeleted"]);
                    document.DeletedBy = rdr["DeletedBy"].ToString();
                    if (rdr["DeletedDate"] != DBNull.Value){
                        document.DeletedDate = Convert.ToDateTime(rdr["DeletedDate"]);
                    }
                    document.LastUpdateBy = rdr["LastUpdateBy"].ToString();
                    if (rdr["LastUpdate"] != DBNull.Value){ 
                    document.LastUpdate = Convert.ToDateTime(rdr["LastUpdate"]);
                    }
                    document.CreatedBy = rdr["CreatedBy"].ToString();
                    if (rdr["CreateDate"] != DBNull.Value){
                        document.CreateDate = Convert.ToDateTime(rdr["CreateDate"]);
                    }
                }
                con.Close();

            }
            return document;

        }


        // To Do - Check to determine whether the document object or the form object is used here.  Gavin 08-01-2018
        public void AddDocument(Document document)
        {

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocDataAdd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ProjectID", System.Data.SqlDbType.Int).Value = document.ProjectID;
                cmd.Parameters.Add("@DocumentTypeID", System.Data.SqlDbType.Int).Value = document.DocumentTypeID;
                cmd.Parameters.Add("@DocumentStatusID", System.Data.SqlDbType.Int).Value = document.DocumentStatusID;
                cmd.Parameters.Add("@UserID", System.Data.SqlDbType.VarChar, 50).Value = document.UserID;
                cmd.Parameters.Add("@DocumentTitle", System.Data.SqlDbType.VarChar, 512).Value = document.DocumentTitle;
                cmd.Parameters.Add("@CurrentRev", System.Data.SqlDbType.VarChar, 30).Value = document.CurrentRev;
                cmd.Parameters.Add("@DocumentID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                con.Open();

                cmd.ExecuteNonQuery();

                document.DocumentID = (int)cmd.Parameters["@DocumentID"].Value;

                con.Close();

            }
        }

        public void EditDocument(Document document) {

            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocDataEdit", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@DocumentID", System.Data.SqlDbType.Int).Value = document.DocumentID;
                cmd.Parameters.Add("@DocumentTitle", System.Data.SqlDbType.VarChar, 512).Value = document.DocumentTitle;
                cmd.Parameters.Add("@DocumentStatusID", System.Data.SqlDbType.Int).Value = document.DocumentStatusID;
                cmd.Parameters.Add("@CurrentRev", System.Data.SqlDbType.VarChar, 30).Value = document.CurrentRev;
                cmd.Parameters.Add("@DocFileName", System.Data.SqlDbType.VarChar, 255).Value = document.DocFileName;
                cmd.Parameters.Add("@AttachmentFileName", System.Data.SqlDbType.VarChar, 255).Value = document.AttachmentFileName;
                cmd.Parameters.Add("@IsCheckedOut", System.Data.SqlDbType.Bit).Value = document.IsCheckedOut;
                cmd.Parameters.Add("@CheckOutBy", System.Data.SqlDbType.VarChar, 50).Value = document.CheckOutBy;
                cmd.Parameters.Add("@CheckOutDate", System.Data.SqlDbType.DateTime).Value = document.CheckOutDate;
                cmd.Parameters.Add("@ReviewBy", System.Data.SqlDbType.VarChar, 50).Value = document.ReviewedBy;
                cmd.Parameters.Add("@ReviewDate", System.Data.SqlDbType.DateTime).Value = document.ReviewDate;
                cmd.Parameters.Add("@SignOffBy", System.Data.SqlDbType.VarChar, 50).Value = document.SignedOffBy;
                cmd.Parameters.Add("@SignOffDate", System.Data.SqlDbType.DateTime).Value = document.SignoffDate;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

            }


        }

        public void FileUpload (int intDocumentID, string strFileName)
        {
            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocFileNameUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@DocumentID", System.Data.SqlDbType.Int).Value = intDocumentID;
                cmd.Parameters.Add("@DocFilename", System.Data.SqlDbType.VarChar, 255).Value = strFileName;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void AttachmentUpload(int intDocumentID, string strAttachFileName)
        {
            using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDocFileAttachmentUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@DocumentID", System.Data.SqlDbType.Int).Value = intDocumentID;
                cmd.Parameters.Add("@AttachmentFilename", System.Data.SqlDbType.VarChar, 255).Value = strAttachFileName;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }
    }
}
