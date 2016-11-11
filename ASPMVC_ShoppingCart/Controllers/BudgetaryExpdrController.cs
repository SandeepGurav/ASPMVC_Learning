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
    public class BudgetaryExpdrController : Controller
    {
        private ASPMVCEntities db = new ASPMVCEntities();
        // GET: BudgetaryExpdr
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            var path = "";
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

                        BudgetaryExpdr BudgetaryExpdr = new BudgetaryExpdr();
                        BudgetaryExpdr BudgetaryExpdrRejected = new BudgetaryExpdr();
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

                                    //BudgetaryExpdr.CEZone = ds.Tables[0].Rows[i][1].ToString();
                                    //BudgetaryExpdr.Month = ds.Tables[0].Rows[i][0].ToString();
                                    //BudgetaryExpdr.Year = ds.Tables[0].Rows[i][2].ToString();
                                    //BudgetaryExpdr. = "Password";
                                    //BudgetaryExpdr.CountryID = 1;
                                    //BudgetaryExpdr.Role = ds.Tables[0].Rows[i][6].ToString();
                                    //if (ModelState.IsValid)
                                    //{
                                    //    try
                                    //    {
                                    //        db.Users.Add(user);
                                    //        db.SaveChanges();
                                    //    }
                                    //    catch
                                    //    {
                                    //        userrejectted = user;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    userrejectted = user;
                                    //}
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