
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Data
{
    class Structure

    {
        private OleDbConnection connection;

        
        public string name;
        public int ID;
        string gender;
        public string title;
        public string period;
        public string performance;


        public Structure()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC\Documents\Employees.accdb;";
            connection = new OleDbConnection(connectionString);
            
        }

        public void SignIn()
        {
            Console.WriteLine("Welcome to Imara School's Employee Management System");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter the username: ".ToUpper());
                name = Console.ReadLine();
                Console.Write("Enter the password: ");
                int password = Convert.ToInt32(Console.ReadLine());

                if (name == "ADMIN" && password == 2023)
                {
                    Console.WriteLine($"{name} has successfully logged in to the system");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Please try again");
                }
            }
        }

        public void ViewEmployeeData()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM employee";
                OleDbDataReader reader = command.ExecuteReader();

                Console.WriteLine("+-----------------------------------------------------------+");
                Console.WriteLine(" ID \t|Name \t|Gender  |Title\t  \t|Period  |Performance");
                Console.WriteLine("+------------------------------------------------------------+");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} \t{reader[1]} \t{reader[2]} \t{reader[3]} \t{reader[4]} \t{reader[5]}");
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void DeleteEmployeeData()
        {
            Console.Write("Enter the name of the employee whose data you want to delete: ");
            name = Console.ReadLine();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM employee WHERE name = '{name}'";
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine($"Successfully deleted {name}'s data.");
                }
                else
                {
                    Console.WriteLine($"{name}'s data not found.");
                }
                Console.WriteLine();

                Console.WriteLine("+-------------------------------------------------------+");
                Console.WriteLine(" ID \t|Name \t|Gender  |Title\t  \t|Period  |Performance");
                Console.WriteLine("+-------------------------------------------------------+");

                command.CommandText = "SELECT * FROM employee";
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} \t{reader[1]} \t{reader[2]} \t{reader[3]} \t{reader[4]} \t{reader[5]}");

                }

                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddEmployeeData()
        {
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC\Documents\Employees.accdb;";

                OleDbConnection connection = new OleDbConnection(connectionString);

                connection.Open();

                Console.Write("Enter employee ID: ");
                ID = int.Parse(Console.ReadLine());

                Console.Write("Enter employee name: ");
                name = Console.ReadLine();

                Console.Write("Enter employee gender: ");
                gender = Console.ReadLine();

                Console.Write("Enter employee job title: ");
                title = Console.ReadLine();

                Console.Write("Enter employee job period of work: ");
                period = Console.ReadLine();

                Console.Write("Enter employee performance: ");
                performance = Console.ReadLine();

                string insertStatement = "INSERT INTO employee (ID, name, gender, title, period, performance) VALUES (@ID, @name, @gender, @title, @period, @performance)";
                OleDbCommand command = new OleDbCommand(insertStatement, connection);

                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@period", period);
                command.Parameters.AddWithValue("@performance", performance);

                int rowsInserted = command.ExecuteNonQuery();

                Console.WriteLine($"{name} has been inserted into the system.");

                string selectStatement = "SELECT TOP 1 ID FROM employee ORDER BY ID DESC";
                OleDbCommand selectCommand = new OleDbCommand(selectStatement, connection);
                int previousID = (int?)selectCommand.ExecuteScalar() ?? 0;

                int newID = previousID + 1;

                ViewEmployeeData();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void Options()
        {
            Console.WriteLine("What would you like to do today? \n 1. To view current employee data \n 2. Delete current employee data \n 3. Add new employee data");
            Console.WriteLine();
            Console.Write("Enter any option from the above list: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Structure obj2 = new Structure();
                    obj2.ViewEmployeeData();
                    break;
                case 2:
                    Structure obj3 = new Structure();
                    obj3.DeleteEmployeeData();
                    break;
                case 3:
                    Structure obj4 = new Structure();
                    obj4.AddEmployeeData();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        public class Show
        {
            public static void Main(string[] args)
            {
                Structure obj4 = new Structure();
                obj4.SignIn();

                Structure obj5 = new Structure();
                obj5.Options();

                Console.ReadLine();
            }

         
        }
        
    } 
    
}



