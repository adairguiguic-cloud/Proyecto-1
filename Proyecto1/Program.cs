using System;

namespace SistemaParqueo
{
    class Program
    {
        static void Main(string[] args)
        {
            string operador = "";
            string turno = "";
            int capacidad = 0, creados = 0, cerrados = 0, tiempo = 0, ocupados = 0;
            decimal total = 0;
            bool activo = false;

            string placa = "";
            int tipo = 0;
            string cliente = "";
            bool vip = false;
            int entrada = 0;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=== SISTEMA DE PARQUEO ===");
            Console.ResetColor();

            Console.WriteLine("Ingrese los datos para iniciar el sistema.\n");

            // OPERADOR
            do
            {
                Console.Write("Nombre del operador: ");
                operador = Console.ReadLine().Trim();

                if (operador == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No puede dejarlo vacío.");
                    Console.ResetColor();
                }

            } while (operador == "");

            // TURNO
            do
            {
                Console.Write("Turno (4 letras o numeros): ");
                turno = Console.ReadLine().Trim();

                if (turno.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Debe tener 4 caracteres.");
                    Console.ResetColor();
                }

            } while (turno.Length != 4);

            // CAPACIDAD
            do
            {
                Console.Write("Capacidad del parqueo (minimo 10): ");

                if (int.TryParse(Console.ReadLine(), out capacidad))
                {
                    if (capacidad < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Debe ser al menos 10.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingrese un numero valido.");
                    Console.ResetColor();
                    capacidad = 0;
                }

            } while (capacidad < 10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSistema listo. Use el menu.\n");
            Console.ResetColor();

            int opcion;

            do
            {
                Menu();

                Console.Write("Seleccione opcion: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            CrearTicket(ref activo, ref placa, ref tipo,
                                         ref cliente, ref vip, ref entrada,
                                         ref creados, ref ocupados,
                                         capacidad, ref tiempo);
                            break;

                        case 2:
                            RegistrarSalida(ref activo, ref placa, ref tipo,
                                             ref cliente, ref vip, ref entrada,
                                             ref cerrados, ref total,
                                             ref ocupados, ref tiempo);
                            break;

                        case 3:
                            MostrarEstado(capacidad, ocupados,
                                           tiempo, total,
                                           creados, cerrados,
                                           activo, placa, cliente);
                            break;

                        case 4:
                            AvanzarTiempo(ref tiempo);
                            break;

                        case 5:
                            MostrarFinal(operador, turno,
                                         capacidad, ocupados,
                                         tiempo, total,
                                         creados, cerrados);
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opcion no valida.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingrese un numero.");
                    Console.ResetColor();
                }

                Console.WriteLine();

            } while (true);
        }

        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("----- MENU -----");
            Console.WriteLine("1. Registrar entrada");
            Console.WriteLine("2. Registrar salida");
            Console.WriteLine("3. Ver estado");
            Console.WriteLine("4. Avanzar tiempo");
            Console.WriteLine("5. Salir");

            Console.ResetColor();
        }

        static void CrearTicket(ref bool activo,
                                ref string placa,
                                ref int tipo,
                                ref string cliente,
                                ref bool vip,
                                ref int entrada,
                                ref int creados,
                                ref int ocupados,
                                int capacidad,
                                ref int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n--- ENTRADA ---");
            Console.ResetColor();

            if (activo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ya hay un ticket activo.");
                Console.ResetColor();
                return;
            }

            if (ocupados >= capacidad)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El parqueo esta lleno.");
                Console.ResetColor();
                return;
            }

            string p;

            do
            {
                Console.Write("Placa (6 a 8 caracteres): ");
                p = Console.ReadLine().Trim().ToUpper();

                if (p.Length < 6 || p.Length > 8 || p.Contains(" "))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Placa incorrecta.");
                    Console.ResetColor();
                }

            }
            while (p.Length < 6 || p.Length > 8 || p.Contains(" "));

            int t;

            do
            {
                Console.WriteLine("Tipo de vehiculo:");
                Console.WriteLine("1 Moto");
                Console.WriteLine("2 Auto");
                Console.WriteLine("3 Pickup");
                Console.Write("Seleccione tipo: ");

                if (int.TryParse(Console.ReadLine(), out t))
                {
                    if (t >= 1 && t <= 3) break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tipo no valido.");
                Console.ResetColor();

            } while (true);

            string nom;

            do
            {
                Console.Write("Nombre del cliente: ");
                nom = Console.ReadLine().Trim();

                if (nom == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No puede estar vacio.");
                    Console.ResetColor();
                }

            }
            while (nom == "");

            string r;

            do
            {
                Console.Write("Es VIP? (s/n): ");
                r = Console.ReadLine().Trim().ToLower();

                if (r == "s" || r == "n") break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Solo s o n.");
                Console.ResetColor();

            }
            while (true);

            activo = true;
            placa = p;
            tipo = t;
            cliente = nom;
            vip = (r == "s");
            entrada = tiempo;
            creados++;
            ocupados++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ticket creado.");
            Console.WriteLine("Entrada registrada en minuto: " + entrada);
            Console.ResetColor();
        }

        static void RegistrarSalida(ref bool activo,
                                    ref string placa,
                                    ref int tipo,
                                    ref string cliente,
                                    ref bool vip,
                                    ref int entrada,
                                    ref int cerrados,
                                    ref decimal total,
                                    ref int ocupados,
                                    ref int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n--- SALIDA ---");
            Console.ResetColor();

            if (!activo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay ticket activo.");
                Console.ResetColor();
                return;
            }

            int minutos = tiempo - entrada;
            int horas = (int)Math.Ceiling(minutos / 60.0);

            decimal tarifa = tipo == 1 ? 5m :
                              tipo == 2 ? 10m : 15m;

            decimal basePago = horas * tarifa;

            if (minutos <= 15)
                basePago = 0;

            if (minutos > 360)
                basePago += 25m;

            decimal pagoFinal = vip ? basePago * 0.9m : basePago;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Minutos: " + minutos);
            Console.WriteLine("Horas cobradas: " + horas);
            Console.WriteLine("Total a pagar: Q" + pagoFinal);
            Console.ResetColor();

            total += pagoFinal;
            cerrados++;
            ocupados--;

            activo = false;
            placa = "";
            cliente = "";
            tipo = 0;
            vip = false;
            entrada = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Salida registrada correctamente.");
            Console.ResetColor();
        }

        static void MostrarEstado(int capacidad,
                                  int ocupados,
                                  int tiempo,
                                  decimal total,
                                  int creados,
                                  int cerrados,
                                  bool activo,
                                  string placa,
                                  string cliente)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n--- ESTADO ---");

            Console.WriteLine("Capacidad: " + capacidad);
            Console.WriteLine("Ocupados: " + ocupados);
            Console.WriteLine("Libres: " + (capacidad - ocupados));
            Console.WriteLine("Tiempo actual: " + tiempo);
            Console.WriteLine("Total recaudado: Q" + total);
            Console.WriteLine("Tickets creados: " + creados);
            Console.WriteLine("Tickets cerrados: " + cerrados);

            if (activo)
                Console.WriteLine("Activo: " + placa + " " + cliente);
            else
                Console.WriteLine("No hay ticket activo.");

            Console.ResetColor();
        }

        static void AvanzarTiempo(ref int tiempo)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n--- AVANZAR TIEMPO ---");
            Console.ResetColor();

            int min;

            do
            {
                Console.Write("Minutos a avanzar: ");

                if (int.TryParse(Console.ReadLine(), out min))
                {
                    if (min >= 1 && min <= 1440)
                    {
                        tiempo += min;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tiempo actualizado.");
                        Console.WriteLine("Tiempo total: " + tiempo);
                        Console.ResetColor();

                        return;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor incorrecto.");
                Console.ResetColor();

            } while (true);
        }

        static void MostrarFinal(string operador,
                                 string turno,
                                 int capacidad,
                                 int ocupados,
                                 int tiempo,
                                 decimal total,
                                 int creados,
                                 int cerrados)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== RESUMEN FINAL ===");

            Console.WriteLine("Operador: " + operador);
            Console.WriteLine("Turno: " + turno);
            Console.WriteLine("Capacidad: " + capacidad);
            Console.WriteLine("Ocupados: " + ocupados);
            Console.WriteLine("Tiempo total: " + tiempo);
            Console.WriteLine("Tickets creados: " + creados);
            Console.WriteLine("Tickets cerrados: " + cerrados);
            Console.WriteLine("Total recaudado: Q" + total);

            Console.WriteLine("\nFin del programa.");
            Console.ResetColor();
        }
    }
}