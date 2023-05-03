using EjercicioSockets_Donoso.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSockets_Donoso
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Obtener clientes
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando Clientes...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion Establecida!");
                        //Protocolo de comunicacion
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            //Leo el mensaje del Cliente
                            mensaje = servidor.Leer();
                            Console.WriteLine("C:{0}", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                //El cliente espera una respuesta
                                Console.WriteLine("Digame lo que quiere decir guruguru");
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("S:{0}", mensaje);
                                servidor.Escribir(mensaje);
                            }
                        }
                        servidor.CerrarConexion();


                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es posible iniciar servidor");
                Console.ReadKey();
            }
        }
    }
}
