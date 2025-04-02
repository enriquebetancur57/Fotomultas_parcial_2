using Fotomultas_parcial_2.Clases;
using Fotomultas_parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fotomultas_parcial_2.Controllers
{
    [RoutePrefix("api/Infracciones")]
    public class InfraccionesController : ApiController
    {

        [HttpGet]
        [Route("ConsultarImagenes")]
        public IQueryable ConsultarImagenes(string Placa)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.ConsultarFotoMultaXVehiculo(Placa);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Infraccion> ConsultarTodos()
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.ConsultarTodos();
        }

        [HttpGet]
        [Route("ConsultarXPlaca")]

        public Infraccion ConsultarXPlaca(string Placa)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.Consultar(Placa);
        }

        [HttpPost]
        [Route("Insertar")]

        public string Insertar([FromBody] Infraccion infraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            Infraccion.infraccion = infraccion;
            return Infraccion.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Infraccion infraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            Infraccion.infraccion = infraccion;
            return Infraccion.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]

        public string Eliminar([FromBody] Infraccion infraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            Infraccion.infraccion = infraccion;
            return Infraccion.Eliminar();
        }

        [HttpDelete]
        [Route("EliminarXPlaca")]
        public string EliminarXPlaca(string Placa)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.EliminarXPlaca(Placa);
        }
    }
}