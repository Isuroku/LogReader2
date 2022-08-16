using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogReader2
{
    public interface IConsoleCommand
    {
        void GetHints(string in_input_text, List<string> out_hints);
        bool Execute(string in_input_text, LogFileControl in_log_control);
    }
}
