using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Razor;

namespace PL.Controllers
{
    public class CargaController : Controller
    {
        [HttpGet]
        public ActionResult Carga()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase csv)
        {
            if (csv != null)
            {
                DataTable dt = new DataTable();
                using (StreamReader reader = new StreamReader(csv.InputStream))
                {
                    //Header
                    string[] headers = reader.ReadLine().Split(',');
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dt.Columns.Add();
                    }
                    //dt.Rows.Add(headers);

                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(',');

                        if (data.Length >= 2)
                        {
                            dt.Rows.Add(data);
                        }
                    }
                }

                bool Correct = BL.CargaMasiva.Cargar(dt);
                if (Correct)
                {
                    ViewBag.Mensaje = "Se insertaron correctamente los registros";

                    StringBuilder csvContent = new StringBuilder();

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            csvContent.Append(item.ToString() + ",");
                        }
                        csvContent.AppendLine();
                    }

                    return File(Encoding.ASCII.GetBytes(csvContent.ToString()), "application/csv", "Data.csv");

                    //return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Hubo un error al insertar los registros";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Mensaje = "Error porfavor adjunta un archivo";
                return PartialView("Modal");
            }
        }
    }
}