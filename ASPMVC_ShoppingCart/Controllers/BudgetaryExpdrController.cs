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
                        //BudgetaryExpdr BudgetaryExpdrRejected = new BudgetaryExpdr();
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                BudgetaryExpdr.CEZone = ds.Tables[0].Rows[0][1].ToString();
                                BudgetaryExpdr.Month = ds.Tables[0].Rows[0][7].ToString();
                                BudgetaryExpdr.Year = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString());

                                for (int i = 2; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                    //{
                                    //     user.Add ds.Tables[0].Rows[i][j].ToString();
                                    //}

                                    BudgetaryExpdr.HeadAcount = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                    BudgetaryExpdr.DemandBE = Convert.ToDecimal(ds.Tables[0].Rows[i][2]);
                                    BudgetaryExpdr.DemandRE = Convert.ToDecimal(ds.Tables[0].Rows[i][3]);
                                    BudgetaryExpdr.Alloted = Convert.ToDecimal(ds.Tables[0].Rows[i][4]);
                                    BudgetaryExpdr.ExpdrLastMonth = Convert.ToDecimal(ds.Tables[0].Rows[i][5]);
                                    BudgetaryExpdr.ExpdrDuringMonth = Convert.ToDecimal(ds.Tables[0].Rows[i][6]);
                                    BudgetaryExpdr.ExpdrTotalYear = Convert.ToDecimal(ds.Tables[0].Rows[i][7]);
                                    BudgetaryExpdr.ExpdrOverAllotment = Convert.ToDecimal(ds.Tables[0].Rows[i][8]);
                                    //BudgetaryExpdr. = "Password";
                                    //BudgetaryExpdr.CountryID = 1;
                                    //BudgetaryExpdr.Role = ds.Tables[0].Rows[i][6].ToString();
                                   if (ModelState.IsValid)
                                    {
                                       try
                                        {
                                            db.BudgetaryExpdrs.Add(BudgetaryExpdr);
                                           db.SaveChanges();
                                       }
                                        catch
                                        {
                                          // BudgetaryExpdrRejected = BudgetaryExpdr;
                                       }
                                    }
                                    else
                                    {
                                        //BudgetaryExpdrRejected = BudgetaryExpdr;
                                    }
                                    //Now we can insert this data to database...
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Import");
        }


    }
}