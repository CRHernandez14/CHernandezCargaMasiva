using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CargaMasiva
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? Update_At { get; set; }
        public string Company_Id { get; set; }

        public static bool Cargar(DataTable dt)
        {
            bool Correct = false;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    using (DL.CHernandezCargarMasivaEntities context = new DL.CHernandezCargarMasivaEntities())
                    {
                        int number = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            number = dt.Rows.IndexOf(row);
                            try
                            {
                                CargaMasiva cargaMasiva = new CargaMasiva();
                                cargaMasiva.Id = (string)row[0];

                                decimal amount = 0;
                                string val = row[3].ToString();

                                bool hasExponential = (val.Contains("E") || val.Contains("e")) && decimal.TryParse(val, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out amount);

                                if (hasExponential)
                                {
                                    cargaMasiva.Amount = amount;
                                }
                                else
                                {
                                    if (decimal.TryParse(val, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
                                    {
                                        cargaMasiva.Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
                                    }
                                }

                                cargaMasiva.Status = (string)row[4];
                                string fecha = row[6].ToString();
                                cargaMasiva.Update_At = fecha != "" ? DateTime.Parse(fecha) : default;
                                cargaMasiva.Company_Id = (string)row[2] != "" && (string)row[2] != "*******" ? (string)row[2] : null;

                                int query = context.CargaMasivaCSV(cargaMasiva.Id,
                                                                   cargaMasiva.Amount,
                                                                   cargaMasiva.Status,
                                                                   cargaMasiva.Update_At,
                                                                   cargaMasiva.Company_Id);
                                if (query > 0)
                                {
                                    Correct = true;
                                }
                                else
                                {
                                    Correct = false;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                Console.WriteLine(number);
                            }
                        }
                    }
                }
                else
                {
                    Correct = false;
                }
            }
            catch (Exception ex)
            {
                Correct = false;
            }
            return Correct;
        }
    }
}
