using System;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            User user = null;

            if (new LoginValidation(username, password, Logger.LogError).ValidateUserInput(ref user))
            {
                Logger.LogActivity("Successful Login");

                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("User password: " + user.Password);
                Console.WriteLine("User faculty number: " + user.FacultyNumber);
                Console.WriteLine("User Role: " + LoginValidation.currentUserRole);
                Console.WriteLine("Created on: " + user.Created);
                Console.WriteLine("Expire on: " + user.ActiveTo);

                switch (LoginValidation.currentUserRole)
                {
                    case UserRoles.ANONYMOUS:
                        Console.WriteLine("Anonymous user...");
                        break;
                    case UserRoles.ADMIN:
                        Console.WriteLine("Administrator...");
                        Menu();
                        break;
                    case UserRoles.INSPECTOR:
                        Console.WriteLine("Inspector...");
                        break;
                    case UserRoles.PROFESSOR:
                        Console.WriteLine("Professor...");
                        break;
                    case UserRoles.STUDENT:
                        Console.WriteLine("Student...");
                        break;
                }
                
            }
        }

        public static void Menu()
        {
            string option; 
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose option:");
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Change role of user");
                Console.WriteLine("2: Change activity of user");
                Console.WriteLine("3: List of users");
                Console.WriteLine("4: View log file");
                Console.WriteLine("5: View current activity");
                Console.WriteLine();


                option = Console.ReadLine();
                switch (option)
                {
                    case "0":
                        Console.WriteLine("Bye!"); break;
                    case "1":
                        SetNewRole();
                        break;
                    case "2":
                        SetActiveToDate();
                        break;
                    case "3":
                        UserData.GetUsers();
                        break;

                    case "4":
                        Logger.ShowLogFile();
                        break;

                    case "5":
                        ShowCurrentSessionActivities();
                        break;

                    default:
                        break;
                        
                }
            }while (Int32.Parse(option) != 0);
        }
        private static void SetNewRole()
        {
            Console.Write("Enter username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter user role: ");
            int userRole = Int32.Parse(Console.ReadLine());
            UserData.AssignUserRole(userName, (UserRoles)userRole);
        }
        private static void SetActiveToDate()
        {
            Console.Write("Enter username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter expire date: ");
            DateTime dateTime = DateTime.Parse(Console.ReadLine());
            UserData.SetUserActiveTo(userName, dateTime);
        }
        private static void ShowCurrentSessionActivities()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> currentSessionActivities = Logger.GetCurrentSessionActivities("INFO");
            foreach (string line in currentSessionActivities)
            {
                sb.Append(line + "\n");
            }
            Console.WriteLine(sb);
        }
    }
}