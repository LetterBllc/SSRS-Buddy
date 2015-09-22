using System;
using System.Collections.Generic;
using System.Text;

namespace SSRSBuddyCMD
{
    class CommandFactory
    {
        public static ICommand CreateCommand(string command)
        {
            switch (command)
            {
                case "ReportDeployer":
                    return new ReportDeployer();
                default:
                    throw new Exception("Command type not recognized");
            }

        }

    }
}
