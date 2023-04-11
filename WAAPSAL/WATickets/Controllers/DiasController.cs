using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WATickets.Models;
using WATickets.Models.Cliente;

namespace WATickets.Controllers
{
    [Authorize]

    public class DiasController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                DateTime time = new DateTime();

                var Dias = db.Dias.Where(a => (filtro.FechaInicial != time ? a.Fecha >= filtro.FechaInicial : true) && (filtro.FechaFinal != time ? a.Fecha <= filtro.FechaFinal : true)).ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Dias = Dias.Where(a => a.Dia.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }

                if(filtro.Codigo1 > 0)
                {
                    Dias = Dias.Where(a => a.idUsuario == filtro.Codigo1).ToList();

                 }

                return Request.CreateResponse(HttpStatusCode.OK, Dias);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Dias/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {



                var Dias = db.Dias.Where(a => a.id == id).FirstOrDefault();


                if (Dias == null)
                {
                    throw new Exception("Este dia no se encuentra registrado");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Dias);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Dias dia)
        {
            try
            {

                var usuarios = db.Login.Where(a => a.Activo == true).ToList();
               
                foreach( var item in usuarios)
                {
                    var time = DateTime.Now.Date;



                    var Dia = db.Dias.Where(a => a.Fecha == time && a.idUsuario  == item.id).FirstOrDefault();

                    if (Dia == null)
                    {
                        Dia = new Dias();

                        Dia.Dia = DateTime.Now.ToString("dddd", new CultureInfo("es-ES"));
                        Dia.Mes = DateTime.Now.ToString("MMMM", new CultureInfo("es-ES"));
                        Dia.Anno = DateTime.Now.Year.ToString();
                        Dia.Fecha = DateTime.Now.Date;
                        Dia.Observaciones = "";
                        Dia.idUsuario = item.id;
                        db.Dias.Add(Dia);
                        db.SaveChanges();

                    }
                   
                }
                


                return Request.CreateResponse(HttpStatusCode.OK, dia);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/Dias/Actualizar")]
        public HttpResponseMessage Put([FromBody] Dias dia)
        {
            try
            {


                var Dia = db.Dias.Where(a => a.id == dia.id).FirstOrDefault();

                if (Dia != null)
                {
                    db.Entry(Dia).State = EntityState.Modified;
                    Dia.Observaciones = dia.Observaciones;

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Dia no existe");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Dia);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}