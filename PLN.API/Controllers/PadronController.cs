﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLN.API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace PLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PadronController : Controller
    {
        private readonly ILogger<PadronController> _logger;

        public PadronController(ILogger<PadronController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Ciudadano/{id}")]
        [ProducesResponseType(typeof(Padron), 200)]
        public ActionResult Ciudadano(int id)
        {
            using (PLNContext db = new PLNContext())
            {
                TextInfo myTI = new CultureInfo("es-CR", false).TextInfo;
                Ciudadano ciudadano = new Ciudadano();
                var data = db.Padrons.Include(c=> c.CodelecNavigation).FirstOrDefault(p => p.Cedula == id);
                if (data != null)
                {
                    ciudadano = new Ciudadano()
                    {
                        Cedula = data.Cedula,
                        Junta = data.Junta,
                        Nombre = myTI.ToTitleCase(data.Nombre.Trim().ToLower()),
                        Apellido1 = myTI.ToTitleCase(data.Apellido1.Trim().ToLower()),
                        Apellido2 = myTI.ToTitleCase(data.Apellido1.Trim().ToLower()),
                        NombreCompleto = data.NombreCompleto,
                        Provincia = myTI.ToTitleCase(data.CodelecNavigation.Provincia.Trim().ToLower()),
                        Canton = myTI.ToTitleCase(data.CodelecNavigation.Canton.Trim().ToLower()),
                        Distrito = myTI.ToTitleCase(data.CodelecNavigation.Distrito.Trim().ToLower()),
                        Fechacaduc = data.Fechacaduc,
                        
                    };
                }
                           
                return Ok(ciudadano);
            }
        }

    }
}
