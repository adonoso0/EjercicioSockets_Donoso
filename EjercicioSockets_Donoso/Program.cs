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
            Console.WriteLine("Iniciando el servidor local en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Esperando clientes
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Esperando Clientes...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion Establecida!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Esperando mensaje del cliente... ");
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            //Se lee el mensaje del cliente
                            mensaje = servidor.Leer();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Cliente dice:[{0}]", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                //El cliente espera una respuesta
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Escriba su respuesta: ");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                mensaje = Console.ReadLine().Trim();
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Mensaje enviado: {0}", mensaje);
                                servidor.Escribir(mensaje);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Esperando respuesta del cliente... ");
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
