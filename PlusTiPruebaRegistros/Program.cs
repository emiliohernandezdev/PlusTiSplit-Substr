using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PlusTiPruebaRegistros
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //MainClass m = new MainClass();
            //m.InitSql();

            Console.WriteLine("Ingresa la ruta del archivo: ");
            String filepath = Console.ReadLine();
            if(filepath == "def"){
                filepath = "/Users/esalazar/Documents/trx.txt";
            }

            char[] separator = { ',' };

            try{

                String datos = File.ReadAllText(filepath);
                Console.WriteLine("Datos del Array: " + datos);
                if (datos != null)
                {

                    if (!File.Exists(filepath))
                    {
                        Console.WriteLine("Archivo inexistente");
                    }
                    else
                    {
                        Console.WriteLine("Con Split: ");
                        TimeSpan stop;
                        TimeSpan start = new TimeSpan(DateTime.Now.Ticks);
                        String[] strList = datos.Split(separator);
                        foreach (String s in strList)
                        {
                            Console.WriteLine(s);
                        }
                        stop = new TimeSpan(DateTime.Now.Ticks);
                        Console.WriteLine(stop.Subtract(start).TotalSeconds + " segundos");


                        Console.WriteLine("==========================================");

                        Console.WriteLine("Con Substring: ");
                        TimeSpan stoptimer;
                        TimeSpan starttimer = new TimeSpan(DateTime.Now.Ticks);

                        Char comma = ',';
                        List<String> filtered = SplitWords(datos, comma);

                        foreach(String dt in filtered){
                            Console.WriteLine(dt);
                        }

                        stoptimer = new TimeSpan(DateTime.Now.Ticks);
                        Console.WriteLine(stoptimer.Subtract(starttimer).TotalSeconds + " segundos");

                    }

                    Console.ReadKey();
                }
            }
            catch(IOException e){
                Console.WriteLine(e.Message);
            }

        }



        static List<String> SplitWords(String Data, char ToDelete)
        {
            String palabra = "";
            Data = Data + ToDelete;
            int length = Data.Length;
            List<String> FilteredWords = new List<String>();
            for (int i = 0; i < length; i++)
            {
                if (Data[i] != ToDelete)
                {
                    palabra = palabra + Data[i];
                }
                else
                {
                    if (palabra.Length != 0)
                    {
                        FilteredWords.Add(palabra);
                    }
                    palabra = "";
                }
            }
            return FilteredWords;
        }



        public void InitSql()
        {
            string dbstring = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;

            dbstring = "Data Source=DESKTOP-LUR90N4;Initial Catalog=DBEstresometro;User ID=sa;Password=guatemala; Connect Timeout=60";
            sql = "SELECT * FROM DatosGuardados";
            connection = new SqlConnection(dbstring);
            try{
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Console.WriteLine("Command executed!");
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

    }
}
