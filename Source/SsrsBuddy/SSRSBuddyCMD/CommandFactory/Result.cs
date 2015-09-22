using System;
using System.Collections.Generic;
using System.Text;

namespace SSRSBuddyCMD
{
    class Result
    {
        private bool _success = false;
        private string _output = string.Empty;
        private string _verbose=string.Empty;

        public bool Successful
        {
            get { return _success; }
            set { _success = value; }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public string Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }
            
    }
}
