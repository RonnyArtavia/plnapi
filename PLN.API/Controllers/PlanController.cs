using MessageBird;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLN.API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace PLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PlanesController : Controller
    {
        private readonly ILogger<PadronController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public PlanesController(ILogger<PadronController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = httpClientFactory;
        }
        [HttpGet("Plan/{token}/{id}")]
        [ProducesResponseType(typeof(Padron), 200)]
        public ActionResult Plan(string token, long id)
        {
            if (token != "actp9l2955022692304pln1")
                return Forbid();
            using (PLNContext db = new PLNContext())
            {
                TextInfo myTI = new CultureInfo("es-CR", false).TextInfo;
                var data = db.PlanesPilotos.FirstOrDefault(p => p.Telefono == id);
                if (data != null)
                {
                    return Ok(data);
                }

                return Ok(new PlanesPiloto());
            }
        }
        [HttpGet("Pruebasms/{token}/{id}")]
        [ProducesResponseType(typeof(Padron), 200)]
        public ActionResult Pruebasms(string token, long id)
        {
            if (token != "actp9l2955022692304pln1")
                return Forbid();
            using (PLNContext db = new PLNContext())
            {
                TextInfo myTI = new CultureInfo("es-CR", false).TextInfo;
                var data = db.PlanesPilotos.FirstOrDefault(p => p.Telefono == id);
                if (data != null)
                {
                    var mensajeEviado = EnviarInstruccionesSMS(data.Telefono, data.Telco);
                    return Ok(data);
                }

                return Ok(new PlanesPiloto());
            }
        }
        [HttpPost("ActivarPlan")]
        [ProducesResponseType(typeof(Padron), 200)]
        public async Task<IActionResult> ActivarPlan(PlanesPiloto data)
        {
            if (data.token != "actp9l2955022692304pln1")
                return Forbid();
            try
            {
                using (PLNContext db = new PLNContext())
                {

                    var plan = db.PlanesPilotos.FirstOrDefault(p => p.Telefono == data.Telefono);
                    if (data != null)
                    {
                        if (plan.RecargaRealizada == null || plan.RecargaRealizada == false)
                        {
                            bool telefonoRecargado = false;
                            //Recargar
                            if (plan.Ubicacion != "Pruebas" && plan.Plan == "Prepago")
                            {
                                telefonoRecargado = await Recargar(plan.Telefono, data.Telco);
                            }
                            plan.RecargaRealizada = telefonoRecargado;
                            plan.Correo = data.Correo;
                            plan.AceptaTerminos = true;
                            plan.FechaAceptaTerminos = DateTime.Now;
                            plan.Cedula = data.Cedula;
                            plan.Modificado = DateTime.Now;
                            plan.Telco = data.Telco;
                            plan.Plan = data.Plan;
                            plan.NombrePadron = data.NombrePadron;
                            db.SaveChanges();

                            if(plan.Ubicacion == "Pruebas")
                                EnviarInstruccionesSMS(plan.Telefono, data.Telco);
                            return Ok("");
                        }
                        else
                        {
                            return NotFound("Plan ya esta activo");
                        }

                    }
                    else
                    {
                        return NotFound("Teléfono no encotado");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound("Error no controlado");
            }

        }
        private async Task<bool> Recargar(long telefono, string proveedor)
        {
            try
            {
                var token = "20jgWSrKDGZ11G6hDBOT-mcMDZGzRZRBY63dgNRq5d21";
                string serviceId = proveedor == "Kolbi" ? "956" : proveedor == "Claro" ? "970" : "973";
                //string apiUrl = $"/api/Pay?token={token}&phone={telefono}&amount=100&serviceId={serviceId}";
                string apiUrl = $"/api/Pay?token={token}&phone={telefono}&amount=100&serviceId={serviceId}";
                var client = _httpClientFactory.CreateClient("RecargasApi");
                var response = await client.PostAsync(apiUrl, null);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        private bool EnviarInstruccionesSMS(long telefono, string proveedor)
        {
            try
            {
                //return true;
                telefono = telefono.ToString().StartsWith("506") ? telefono : long.Parse("506" + telefono.ToString());
                StringBuilder msg = new StringBuilder();
                List<long> telfonos = new List<long> { telefono };
                msg.Append("Tu recarga de 600.00 fue exitosa. Tu saldo es de 2027.72 colones.");
                // msg.AppendLine("Marca *888# y tecla de llamada, escogé la opción 3.");

                Client client = Client.CreateDefault("NShZAaZVqL8LdR745VdeKnbhV");
                var result = client.SendMessage("PLN", msg.ToString(), telfonos.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }


        }
    }
}
