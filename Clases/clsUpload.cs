using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Fotomultas_parcial_2.Clases
{
    public class clsUpload
    {
        public HttpRequestMessage Request { get; set; }
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public async Task<HttpResponseMessage> GrabarArchivo()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                List<string> Archivos = new List<string>();
                foreach (MultipartFileData file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    if (File.Exists(Path.Combine(root, fileName)))
                    {
                        File.Delete(file.LocalFileName);
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe");
                    }
                    Archivos.Add(fileName);
                    File.Move(file.LocalFileName, Path.Combine(root, fileName));
                }
                string Respuesta = ProcesarArchivos(Archivos);
                return Request.CreateResponse(HttpStatusCode.OK, "Archivo subido correctamente");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al cargar el archivo: " + ex.Message);
            }
        }
        private string ProcesarArchivos(List<string> Archivos)
        {
            switch (Proceso.ToUpper())
            {
                case "FotoMulta":
                    clsFotoInfraccion FotoMulta = new clsFotoInfraccion();
                    FotoMulta.idFotoMulta = Datos;
                    FotoMulta.Archivos = Archivos;
                    return FotoMulta.GrabarFotoMulta();
                default:
                    return "Proceso no definido";
            }
        }
    }
}