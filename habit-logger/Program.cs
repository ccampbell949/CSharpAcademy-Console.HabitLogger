using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=c:\repos\CSharpAcademy-Console.HabitLogger\habit-logger\habit-Tracker.db";
        static void Main(string[] args)
        {

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id  INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("Type 1 to View all records");
                Console.WriteLine("Type 2 to Insert Record");
                Console.WriteLine("Type 3 to Delete Record");
                Console.WriteLine("Type 4 to Update Record");
                Console.WriteLine("---------------------------------------------\n");

                string commandInput = Console.ReadLine();

                switch(commandInput)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        break;

                    // case "1":
                    //     GetAllRecords();
                    //     break;

                    case "2":
                        Insert();
                        break;

                    // case "3":
                    //     Delete();
                    //     break;

                    // case "4":
                    //     Update();
                    //     break;
                }

            }
        }

        private static void Insert()
        {
            string date = GetDateInput();

            int quantity = GetNumberInput("\n\nPlease insert the number of glasses or other measure of your choice (no decimals allowed)\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tablecmd = connection.CreateCommand();
                tablecmd.CommandText = 
                $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";

                tablecmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        private static string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu");
            string dateInput = Console.ReadLine();

            if (dateInput == "0")
            {
                GetUserInput();
            }
            return dateInput;
        }

        internal static int GetNumberInput(string message)
        {
            Console.Write(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") GetUserInput();

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }
    }
}
