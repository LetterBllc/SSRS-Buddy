using System;
using System.Collections.Generic;
using System.Text;

namespace SSRSBuddyCMD
{
    class Program
    {
        public const string INVALID_ARGUMENTS = "Invalid arguments. Use SSRSBuddyCMD HELP to get help.";

        private static ICommand mycommand;

        static void Main(string[] args)
        {
            Console.WriteLine("SSRSBuddyCMD : SSRSBuddy Command line tool");

            if (args.Length==0)
            {
                Console.WriteLine(INVALID_ARGUMENTS);
            }

            switch (args[0]) {
                case "HELP":
                    Console.WriteLine("Available commands :");
                    Console.WriteLine("DEPLOY \t\tDeploy reports and report models");
                    Console.WriteLine("CLONE \t\tClone reports");
                    Console.WriteLine("MERGE \t\tMerge Report model and datasource view");
                    Console.WriteLine("Use HELP on these commands to get help e.g. SSRSBuddyCMD DEPLOY HELP");
                    break;
                case "DEPLOY":
                    mycommand = CommandFactory.CreateCommand("DEPLOY");
                    break;

                case "CLONE":

                    break;

                case "MERGE" :

                    break;
                default :
                    Console.WriteLine(INVALID_ARGUMENTS);
                    break;
            }

            //execute 
            mycommand.ValidateArgs();
            Result myResult = mycommand.Execute();
            Console.WriteLine(myResult.Output);

            // return exit code
            if (myResult.Successful)
                Environment.Exit(0);
            else
                Environment.Exit(-1);

        }
    }
}
