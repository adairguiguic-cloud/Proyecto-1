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

            Console.WriteLine("=== SISTEMA DE PARQUEO ===");

            do
            {
                Console.Write("Operador: ");
                operador = Console.ReadLine().Trim();
                if (operador == "") Console.WriteLine("Dato inválido.");
            }
            while (operador == "");

            do
            {
                Console.Write("Turno (4): ");
                turno = Console.ReadLine().Trim();
                if (turno.Length != 4) Console.WriteLine("Debe tener 4 caracteres.");
            }
            while (turno.Length != 4);

            do
            {
                Console.Write("Capacidad (>=10): ");
                if (int.TryParse(Console.ReadLine(), out capacidad))
                {
                    if (capacidad < 10) Console.WriteLine("Muy pequeña.");
                }
                else
                {
                    Console.WriteLine("Número inválido.");
                    capacidad = 0;
                }
            }
            while (capacidad < 10);

            int opcion;

            do
            {
                Menu();
                Console.Write("Opción: ");

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
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese número.");
                }

                Console.WriteLine();

            } while (true);
        }

        static void Menu()
        {
            Console.WriteLine("1. Entrada");
            Console.WriteLine("2. Salida");
            Console.WriteLine("3. Estado");
            Console.WriteLine("4. Tiempo");
            Console.WriteLine("5. Salir");
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
            if (activo)
            {
                Console.WriteLine("Ya hay ticket.");
                return;
            }

            if (ocupados >= capacidad)
            {
                Console.WriteLine("Parqueo lleno.");
                return;
            }

            string p;

            do
            {
                Console.Write("Placa: ");
                p = Console.ReadLine().Trim().ToUpper();

                if (p.Length < 6 || p.Length > 8 || p.Contains(" "))
                    Console.WriteLine("Placa inválida.");

            }
            while (p.Length < 6 || p.Length > 8 || p.Contains(" "));

            int t;

            do
            {
                Console.WriteLine("1 Moto");
                Console.WriteLine("2 Auto");
                Console.WriteLine("3 Pickup");
                Console.Write("Tipo: ");

                if (int.TryParse(Console.ReadLine(), out t))
                {
                    if (t >= 1 && t <= 3) break;
                }

                Console.WriteLine("Dato inválido.");

            } while (true);

            string nom;

            do
            {
                Console.Write("Cliente: ");
                nom = Console.ReadLine().Trim();

                if (nom == "") Console.WriteLine("Vacío.");

            }
            while (nom == "");

            string r;

            do
            {
                Console.Write("VIP s/n: ");
                r = Console.ReadLine().Trim().ToLower();

                if (r == "s" || r == "n") break;

                Console.WriteLine("Solo s o n.");

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

            Console.WriteLine("Ticket creado correctamente.");
            Console.WriteLine("Placa: " + placa);
            Console.WriteLine("Cliente: " + cliente);
            Console.WriteLine("Entrada minuto: " + entrada);
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
            if (!activo)
            {
                Console.WriteLine("No hay ticket.");
                return;
            }

            int minutos = tiempo - entrada;
            int horas = (int)Math.Ceiling(minutos / 60.0);

            decimal tarifa = tipo == 1 ? 5m : tipo == 2 ? 10m : 15m;
            decimal basePago = horas * tarifa;

            if (minutos <= 15) basePago = 0;

            if (minutos > 360) basePago += 25m;

            decimal pagoFinal = vip ? basePago * 0.9m : basePago;

            Console.WriteLine("Minutos estacionados: " + minutos);
            Console.WriteLine("Horas cobradas: " + horas);
            Console.WriteLine("Tarifa por hora: Q" + tarifa);
            Console.WriteLine("Monto final: Q" + pagoFinal);

            total += pagoFinal;
            cerrados++;
            ocupados--;

            activo = false;
            placa = "";
            cliente = "";
            tipo = 0;
            vip = false;
            entrada = 0;
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
            Console.WriteLine("Capacidad: " + capacidad);
            Console.WriteLine("Ocupados: " + ocupados);
            Console.WriteLine("Libres: " + (capacidad - ocupados));
            Console.WriteLine("Tiempo: " + tiempo);
            Console.WriteLine("Recaudado: Q" + total);
            Console.WriteLine("Creados: " + creados);
            Console.WriteLine("Cerrados: " + cerrados);

            if (activo)
                Console.WriteLine("Activo: " + placa + " " + cliente);
            else
                Console.WriteLine("Sin ticket.");
        }

        static void AvanzarTiempo(ref int tiempo)
        {
            int min;

            do
            {
                Console.Write("Minutos: ");

                if (int.TryParse(Console.ReadLine(), out min))
                {
                    if (min >= 1 && min <= 1440)
                    {
                        tiempo += min;
                        Console.WriteLine("Tiempo actualizado.");
                        Console.WriteLine("Tiempo total: " + tiempo);
                        return;
                    }
                }

                Console.WriteLine("Valor inválido.");

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
            Console.WriteLine("=== RESUMEN ===");
            Console.WriteLine("Operador: " + operador);
            Console.WriteLine("Turno: " + turno);
            Console.WriteLine("Capacidad: " + capacidad);
            Console.WriteLine("Ocupados: " + ocupados);
            Console.WriteLine("Tiempo: " + tiempo);
            Console.WriteLine("Tickets creados: " + creados);
            Console.WriteLine("Tickets cerrados: " + cerrados);
            Console.WriteLine("Total recaudado: Q" + total);
        }
    }
}