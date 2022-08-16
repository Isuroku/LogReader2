using System;
using System.Collections.Generic;
using System.Text;

namespace LogReader2
{
    public class ConsoleCommandSelectText : ConsoleCommandBase
    {
        public ConsoleCommandSelectText() : base("select") { }

        public override bool Execute(string in_input_text, LogFileControl in_log_control)
        {
            if (in_log_control == null)
                return false;

            SParsedCommand parsed_command;
            ParseText(in_input_text, out parsed_command);

            if(string.IsNullOrEmpty(parsed_command.CommandText) 
                || !string.Equals(parsed_command.CommandText, _command_text, StringComparison.InvariantCultureIgnoreCase)
                || string.IsNullOrEmpty(parsed_command.SelectedText))
                return false;

            return in_log_control.SelectText(parsed_command.SelectedText);
        }
    }
}
