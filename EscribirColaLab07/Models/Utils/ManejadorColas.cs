using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace EscribirColaLab07.Models.Utils
{
    public class ManejadorColas
    {
        public static ManejadorColas _instancia;
        public static ManejadorColas Instancia
        {
            get
            
                {
                    if (_instancia == null)_instancia=new ManejadorColas();
                    return _instancia;
                }
            
    }


        public bool CrearCola(String conn, String nombre, int tamano, int tiempo)
        {
            var conexion = NamespaceManager.CreateFromConnectionString(conn);

            if (conexion.QueueExists(nombre))
            {
                return false;
            }
            QueueDescription qd= new QueueDescription(nombre);
            qd.MaxSizeInMegabytes = tamano;
            qd.DefaultMessageTimeToLive= new TimeSpan(0,0,tiempo);
            try
            {
                conexion.CreateQueue(qd);
                return true;
            }
            catch (Exception exception)
            {
                
                Console.WriteLine(exception);
            }
            return false;
        }

        public void Enviar(String conn, String nombre, Dictionary<string, string> parametros, String texto)
        {
            var cl = QueueClient.CreateFromConnectionString(conn, nombre);
            var msg= new BrokeredMessage(texto);
            foreach (var key in parametros.Keys)
            {
                msg.Properties[key] = parametros[key];
            }
            cl.Send(msg);
        }

    }
}