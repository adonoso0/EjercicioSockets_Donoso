﻿using ClienteSocketUtils.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectando a {0}:{1}", ip, puerto);
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado");

                string mensaje = "";
                while (mensaje.ToLower() != "chao")
                {
                    Console.WriteLine("Escriba el mensaje");
                    mensaje = Console.ReadLine().Trim();
                    Console.WriteLine("C:{0}", mensaje);
                    clienteSocket.Escribir(mensaje);
                    if (mensaje.ToLower() != "chao")
                    {
                        mensaje = clienteSocket.Leer();
                        Console.WriteLine("S:{0}", mensaje);
                    }
                }
                clienteSocket.Desconectar();

            }
            else
            {
                Console.WriteLine("Error de conexion");
            }

        }
    }
}
