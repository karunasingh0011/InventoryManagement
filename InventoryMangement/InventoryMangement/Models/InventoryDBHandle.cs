using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace InventoryMangement.Models
{
    public class InventoryDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["InvDb"].ToString();
            con = new SqlConnection(constring);
        }

       
        public bool AddInventory(InvClass invmodel,HttpPostedFileBase file )
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewInventoryItem", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", invmodel.Name);
            cmd.Parameters.AddWithValue("@Description", invmodel.Description);
            cmd.Parameters.AddWithValue("@Price", invmodel.Price);
          
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(HttpContext.Current.Server.MapPath("~/Img/"),filename);
                file.SaveAs(imgpath);

                UploadImage.Crop(320, 240, file.InputStream, Path.Combine(HttpContext.Current.Server.MapPath("~/Img/Thumb/") + filename));
            }
            cmd.Parameters.AddWithValue("@Image", "~/Img/Thumb/" + file.FileName);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteInventory(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteInventory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@InvId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<InvClass> GetInventory()
        {
            connection();
            List<InvClass> Inventorylist = new List<InvClass>();

            SqlCommand cmd = new SqlCommand("GetInventoryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Inventorylist.Add(
                    new InvClass
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Description = Convert.ToString(dr["Description"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                       Image = Convert.ToString(dr["Photo"])

            });
            }
            return Inventorylist;
        }

        public class UploadImage
        {
            public static void Crop(int Width, int Height, Stream streamImg, string saveFilePath)
            {
                Bitmap sourceImage = new Bitmap(streamImg);
                using (Bitmap objBitmap = new Bitmap(Width, Height))
                {
                    objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                    {
                       
                        objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                      
                        objBitmap.Save(saveFilePath);
                    }
                }
            }
        }


    }
}