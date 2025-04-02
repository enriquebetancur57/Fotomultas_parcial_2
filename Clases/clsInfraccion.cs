using Fotomultas_parcial_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Fotomultas_parcial_2.Clases
{
    public class clsInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities(); //para acceder a la base de datos
        public Infraccion infraccion { get; set; } //para acceder o manipular los atributos
        public string Insertar()
        {
            try
            {
                dbExamen.Infraccions.Add(infraccion); //Agrega una nueva infracción(Insert Into)
                dbExamen.SaveChanges();//Guarda los cambios en la BD
                return "Infracción insertada Correctamente"; //Mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar la infracción: " + ex.Message; //Mensaje de error
            }
        }

        public string Actualizar()
        {
            //Para asegurar que sea un update primero se debe conultar el empleado
            Infraccion inf = Consultar(infraccion.PlacaVehiculo);//Consulta empleado por su doc
            if (inf == null)
            {
                //La infracción no exite, se debe insertar o placa no es valido
                return "La placa no es valida o no existe una infracción en el sistema";
            }
            dbExamen.Infraccions.AddOrUpdate(infraccion); //Actualiza un empleado en la tabla
            dbExamen.SaveChanges();
            return "Se Actualizó la infracción Correctamente";
        }

        public Infraccion Consultar(string Placa)
        {
            try
            {
                Infraccion inf = dbExamen.Infraccions.FirstOrDefault(e => e.PlacaVehiculo == Placa);
                return inf; //Devuelve la placa que se consulto
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Infraccion> ConsultarTodos()
        {
            return dbExamen.Infraccions.OrderBy(e => e.PlacaVehiculo).ToList();
        }
        public string Eliminar()
        {
            try
            {
                //Se debe consultar primeramente la infracción para poder eliminarla
                Infraccion inf = Consultar(infraccion.PlacaVehiculo);
                if (inf == null)
                {
                    //Si no existe, debe de insertarse una placa registrada o con infracciones
                    return "La placa no es valida";
                }
                dbExamen.Infraccions.Remove(inf); //Elimina la infracción
                dbExamen.SaveChanges();//Guarda los cambios
                return "Se eliminó la infracción correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message; //Mensaje de error
            }
        }

        public string EliminarXPlaca(string Placa)
        {
            try
            {
                //Se debe consultar la placa
                Infraccion inf = Consultar(Placa);
                if (inf == null)
                {
                    //Si no existe, debe de insertarse una placa registrada o con infracciones
                    return "La placa no es valida";
                }
                dbExamen.Infraccions.Remove(inf); //Elimina la infracción
                dbExamen.SaveChanges();//Guarda los cambios
                return "Se eliminó la infracción correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message; //Mensaje de error
            }
        }

        public IQueryable ConsultarFotoMultaXVehiculo(string Placa)
        {
         return from V in dbExamen.Set<Vehiculo>()
                join I in dbExamen.Set<Infraccion>()
                on V.Placa equals I.PlacaVehiculo
                join F in dbExamen.Set<FotoInfraccion>()
                on I.idFotoMulta equals F.idInfraccion
                where V.Placa == Placa
                select new
                {
                    Placa = V.Placa,
                    Marca = V.Marca,
                    TipoVehiculo = V.TipoVehiculo,
                    Color = V.Color,
                    FechaInfraccion = I.FechaInfraccion,
                    idFotoMulta = I.idFotoMulta,
                    NombreFoto= F.NombreFoto
                };
        }
    }
}

    