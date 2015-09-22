using System;
using System.Collections.Generic;
using System.Text;

namespace SSRSBuddyCMD
{
    interface ICommand
    {
        void SetArgs(string[] args);
        bool ValidateArgs();
        Result Execute();

        //string Describe();

    }
}
