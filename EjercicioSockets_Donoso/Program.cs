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
                    //Esperando clientes
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando Clientes...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion Establecida!");
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            //Se lee el mensaje del cliente
                            mensaje = servidor.Leer();
                            Console.WriteLine("Cliente:[{0}]", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                //El cliente espera una respuesta
                                Console.WriteLine("Escriba su respuesta: ");

                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("Mensaje enviado: {0}", mensaje);
                                servidor.Escribir(mensaje);
                            }
                        }
                        servidor.CerrarConexion();


                    }
                }
            }
            else
            {
                Console.WriteLine("No es posible iniciar servidor");
                Console.ReadKey();
            }
        }
    }
}
