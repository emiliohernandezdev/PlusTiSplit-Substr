using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PlusTiPruebaRegistros
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            MainClass m = new MainClass();
            //m.InitSql();
            Stopwatch SplitWatch = new Stopwatch();
            Stopwatch ListWatch = new Stopwatch();
            Stopwatch SubstrWatch = new Stopwatch();


            Console.WriteLine("Ingresa la ruta del archivo: ");
            String filepath = Console.ReadLine();
            if(filepath == "def"){
                filepath = "/Users/esalazar/Documents/trx.txt";
            }

            char[] separator = { ',' };

            try{

                String datos = File.ReadAllText(filepath);
                if (datos != null)
                {

                    if (!File.Exists(filepath))
                    {
                        Console.WriteLine("Archivo inexistente");
                    }
                    else
                    {
                        Console.WriteLine("Con Split: ");
                        SplitWatch.Start();
                        String[] strList = datos.Split(separator);
                        foreach (String s in strList)
                        {
                            Console.WriteLine(s);
                        }
                        SplitWatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", SplitWatch.Elapsed);



                        Console.WriteLine("==========================================");

                        Console.WriteLine("Con Metodo list: ");

                        Char comma = ',';
                        List<String> filtered = SplitWords(datos, comma);

                        ListWatch.Start();

                        foreach(String dt in filtered){
                            Console.WriteLine(dt);
                        }

                        ListWatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", ListWatch.Elapsed);


                        Console.WriteLine("==========================================");

                        Console.WriteLine("Con Metodo Substring:");


                        List<String> substr = Substr(datos, comma);
                        SubstrWatch.Start();

                        foreach(String sb in substr){
                            Console.WriteLine(sb);
                        }

                        SubstrWatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", SubstrWatch.Elapsed);

                    }
                    Thread.Sleep(5000);
                }
            }
            catch(IOException e){
                Console.WriteLine(e.Message);
            }

        }

        static List<String> Substr(String TodosLosDatos, char ToDelete){
            List<String> palabras = new List<String>();
            int length = (TodosLosDatos = TodosLosDatos + ',').Length;
            int i = 0;

            int lastFound = 0;
            for (i = 0; i < length; i++) {
                if (TodosLosDatos[i] != ',') continue;
                String found = TodosLosDatos.Substring(lastFound, i - lastFound);
                lastFound = i + 1;
                palabras.Add(found);
            }
            return palabras;
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
