using Fotomultas_parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotomultas_parcial_2.Clases
{
    public class clsFotoInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public string idInfraccion { get; set; }
        public List<string> Archivos { get; set; }
        public string GrabarFotoMulta()
        {
            try
            {
                if (Archivos.Count > 0)
                {
                    foreach (string Archivo in Archivos)
                    {
                        FotoInfraccion Foto = new FotoInfraccion();
                        Foto.idInfraccion = Convert.ToInt32(idInfraccion);
                        Foto.NombreFoto = Archivo;
                        dbExamen.FotoInfraccions.Add(Foto);
                        dbExamen.SaveChanges();
                    }
                    return "Imagenes guardadas correctamente";
                }
                else
                {
                    return "No se han subido imagenes";
                }
            }
            catch (Exception ex)
            {
                return "Error al grabar las imagenes: " + ex.Message;
            }
        }
    }
}