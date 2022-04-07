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

    public class ComidasController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {

                var Comidas = db.Comidas.ToList();

                if (filtro.Codigo1 > 0)
                {
                    Comidas = Comidas.Where(a => a.idDia == filtro.Codigo1).ToList();
                }



                return Request.CreateResponse(HttpStatusCode.OK, Comidas);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Comidas/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {



                var Comida = db.Comidas.Where(a => a.id == id).FirstOrDefault();


                if (Comida == null)
                {
                    throw new Exception("Esta comida no se encuentra registrado");
                }

                return Request.CreateResponse(HttpStatusCode.OK, Comida);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Comidas comida)
        {
            try
            {

                 
                var Comida = db.Comidas.Where(a => a.id == comida.id).FirstOrDefault();

                if (Comida == null)
                {
                    Comida = new Comidas();

                    Comida.idDia = comida.idDia;
                    Comida.Descripcion = comida.Descripcion;
                    Comida.Foto = comida.Foto;
                    Comida.Calorias = comida.Calorias;
                   

                    db.Comidas.Add(Comida);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Esta comida YA existe");
                }


                return Request.CreateResponse(HttpStatusCode.OK, Comida);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}