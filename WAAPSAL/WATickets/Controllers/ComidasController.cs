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
        public HttpResponseMessage Post([FromBody] ComidaViewModel[] comida)
        {
            var t = db.Database.BeginTransaction();
            try
            {


                var idDia = comida.FirstOrDefault().idDia;

                var comidasEliminar = db.Comidas.Where(a => a.idDia == idDia).ToList();

                foreach(var item in comidasEliminar)
                {
                    db.Comidas.Remove(item);
                    db.SaveChanges();
                }

                foreach(var item in comida)
                {
                    var Comida = db.Comidas.Where(a => a.id == item.id).FirstOrDefault();

                    if (Comida == null)
                    {
                        Comida = new Comidas();

                        Comida.idDia = item.idDia;
                        Comida.Descripcion = item.Descripcion;
                        try
                        {
                            byte[] hex = Convert.FromBase64String(item.Foto.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", ""));
                            Comida.Foto = hex;
                        }
                        catch (Exception)
                        {

                            
                        }
                       
                        Comida.Calorias = item.Calorias;


                        db.Comidas.Add(Comida);
                        db.SaveChanges();

                    }
                    else
                    {
                        throw new Exception("Esta comida YA existe");
                    }
                }

                t.Commit();


                return Request.CreateResponse(HttpStatusCode.OK, comida);
            }
            catch (Exception ex)
            {
                t.Rollback();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}