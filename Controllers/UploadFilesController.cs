using Fotomultas_parcial_2.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Fotomultas_parcial_2.Controllers
{
    [RoutePrefix("UploadFiles")]
    public class UploadFilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> GrabarArchivo(HttpRequestMessage Request, string Datos, string Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.Request = Request;
            UploadFiles.Datos = Datos;
            return await UploadFiles.GrabarArchivo();
        }
    }
}