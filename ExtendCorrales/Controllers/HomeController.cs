using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExtendCorrales.Models;
using ExtendCorrales.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;

namespace ExtendCorrales.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExtendCorralesDBContext _context;
        public HomeController(ExtendCorralesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ModelViewTropa modelView = new ModelViewTropa();

            modelView.List = GetNroTropas().List;

            return View(modelView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Devuelve la información de la tropa registrada en la tabla LLEGADA_TROPA_RESUMEN
        /// de la DB TwinsDb
        /// </summary>
        /// <param name="TropaId"></param>
        /// <returns>ExtendCorral</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetTropaInfo([FromBody]int TropaId)
        {
            ExtendCorral extendCorral = new ExtendCorral();
            string connStr = _context.Database.GetDbConnection().ConnectionString;
            string storedProcedure = "EC_getTropaInfo";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcedure, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tropaId", SqlDbType.Int);
                cmd.Parameters["@tropaId"].Value = TropaId;

                try
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    extendCorral.TropaId = (int)rdr["TropaID"];
                    extendCorral.NroTropa = (int)rdr["NRO_TROPA"];
                    extendCorral.FechaLlegada = (DateTime)rdr["FECHA_LLEGADA"];
                    extendCorral.HoraLlegada = (string)rdr["HORA_LLEGADA"];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return new JsonResult(extendCorral);
        }

        /// <summary>
        /// Devuelve los numeros de tropas no faenadas aún
        /// </summary>
        /// <returns>List<ModelViewTropa></returns>

        public ModelViewTropa GetNroTropas()
        {
            ModelViewTropa modelViewTropaList = new ModelViewTropa();
            string connStr = _context.Database.GetDbConnection().ConnectionString;
            string storedProcedure = "EC_getTropasNoFaenadas";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcedure, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ModelViewTropaItem modelViewTropa = new ModelViewTropaItem();
                            modelViewTropa.TropaId = (int)rdr["TropaID"];
                            modelViewTropa.TropaNro = (int)rdr["NRO_TROPA"];
                            DateTime dateText = (DateTime)rdr["FECHA_LLEGADA"];
                            modelViewTropa.FechaLlegada = dateText.ToString("dd-MM-yyyy");

                            modelViewTropaList.List.Add(modelViewTropa);
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return modelViewTropaList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store([FromBody]JObject data)
        {
            ExtendCorral extendCorral = new ExtendCorral();

            int state = 0;

            extendCorral.TropaId = Convert.ToInt32(data["tropaId"]);
            extendCorral.NroTropa = Convert.ToInt32(data["nroTropa"]);
            extendCorral.FechaLlegada = Convert.ToDateTime(data["fechaLlegada"]);
            extendCorral.HoraLlegada = data["horaLlegada"].ToString();
            extendCorral.Corral = Convert.ToInt32(data["corral"]);
            extendCorral.Raza = data["raza"].ToString();
            extendCorral.Categoria = data["categoria"].ToString();
            extendCorral.Edad = data["edad"].ToString();
            extendCorral.PesoGeneral = data["pesoGeneral"].ToString();
            extendCorral.EstadoGeneral = data["estadoGeneral"].ToString();
            extendCorral.Condicion = data["condicionLlegada"].ToString();

            _context.ExtendCorrales.Add(extendCorral);

            try
            {
                state = _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                state = 0;
            }

            return new JsonResult(state);

        }

    }
}
