using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivoBinarioEmpleados
    {
        //declaración de flujos
        BinaryWriter bw = null; //flujo de salida
        BinaryReader br = null; //flujo de entrada

        //campo de clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //variable local metodo
            char resp;
            try
            {
                //creación del flujo para escribir datos del archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //captura de datos

                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Dirección del Empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Telefono del Empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos del archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro Registro (s/n)?");
                    resp = Char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }

            catch (IOException e)
            {
                Console.WriteLine("\nError : "+ e.Message);
                Console.WriteLine("\nRuta : " + e.StackTrace);
            }

            finally
            {
                if (bw != null) bw.Close(); //cierra el flujo - escritura
                Console.Write("\nPresione <Enter> para terminar la Escritura de datos y regresar al Menu");
                Console.ReadKey();
            }
        }

        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //lectura de registros mientras no llegue EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();


                        //muestra los datos
                        Console.WriteLine("Numero del Empleado    :" + NumEmp);
                        Console.WriteLine("Nombre del Empleado    :" + Nombre);
                        Console.WriteLine("Dirección del Empleado : " + Direccion);
                        Console.WriteLine("Teléfono del Empleado : " + Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado : " + DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado : ,{0:C}" + SalarioDiario);
                        Console.WriteLine("Sueldo Total del Empleado : ,{0:C}" + DiasTrabajados * SalarioDiario);
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear ();
                    Console.WriteLine("\n\nEl Archivo" + Archivo + "No Existe en el Disco!!");
                    Console.WriteLine("\nPresione <Enter> para Continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.WriteLine("\nPresione <enter> para terminar la Lectura de datos y regresar al Menu");
                Console.ReadKey();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //declaracion de variables auxiliares
            string Arch = null;
            int opcion;

            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();
            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: "); Arch = Console.ReadLine();
                            
                             //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: "); 
                            Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del  Programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione  < enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}