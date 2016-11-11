using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASPMVC_ShoppingCart.Models;
using ASPMVC_ShoppingCart.Security;

namespace ASPMVC_ShoppingCart.Controllers
{
    public class TestController : Controller
    {
        private ASPMVCEntities db = new ASPMVCEntities();
        // GET: Test
        public ActionResult Index()
        {
            ViewData["message"] = 5;
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(User User)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Countries.Add(User);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(country);
        //}
        public ActionResult Import(HttpPostedFileBase file)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            var path="";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                 path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
            

            DataSet ds = new DataSet();

            string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";

            using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
            {
                conn.Open();
                using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                {
                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    string query = "SELECT * FROM [" + sheetName + "]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                    //DataSet ds = new DataSet();
                    adapter.Fill(ds, "Items");
                    
                    User user=new User();
                    User userrejectted = new User();
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            
                            for (int i = 2; i < ds.Tables[0].Rows.Count; i++)
                            {
                                //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                //{
                                //     user.Add ds.Tables[0].Rows[i][j].ToString();
                                //}
                                
                                user.EmailID = ds.Tables[0].Rows[i][1].ToString();
                                user.Name = ds.Tables[0].Rows[i][0].ToString();
                                user.ContactNo = ds.Tables[0].Rows[i][2].ToString();
                                user.Password = "Password";
                                user.CountryID = 1;
                                user.Role = ds.Tables[0].Rows[i][6].ToString();
                                if (ModelState.IsValid)
                                {
                                    try
                                    {
                                        db.Users.Add(user);
                                        db.SaveChanges();
                                    }
                                    catch
                                    {
                                        userrejectted = user;
                                    }
                                }
                                else
                                {
                                    userrejectted = user;
                                }
                                //Now we can insert this data to database...
                            }
                        }
                    }
                }
            }
            }
            return RedirectToAction("Index");
        }
    }
}