using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogReader2
{
    public abstract class ConsoleCommandBase : IConsoleCommand
    {
        protected string _command_text;

        public ConsoleCommandBase(string in_command_text)
        {
            _command_text = in_command_text;
        }

        public abstract bool Execute(string in_input_text, LogFileControl in_log_control);

        public void GetHints(string in_input_text, List<string> out_hints)
        {
            SParsedCommand parsed_command;
            ParseText(in_input_text, out parsed_command);

            if (string.IsNullOrEmpty(parsed_command.CommandText)
                || HasCommonStart(parsed_command.CommandText, _command_text))
            {
                StringBuilder sb = new StringBuilder(_command_text);
                if (!string.IsNullOrEmpty(parsed_command.SelectedText))
                {
                    sb.Append(_space);
                    sb.Append(parsed_command.SelectedText);
                }

                out_hints.Add(sb.ToString());
            }
        }

        protected struct SParsedCommand
        {
            public string CommandText;
            public string SelectedText;
        }

        protected char[] _spaces = { ' ' };
        protected char _space =  ' ' ;
        protected void ParseText(string in_input_text, out SParsedCommand out_parsed_command)
        {
            out_parsed_command.CommandText = string.Empty;
            out_parsed_command.SelectedText = string.Empty;

            if (string.IsNullOrEmpty(in_input_text))
                return;

            if(in_input_text[0] == _space) //space
            {
                out_parsed_command.SelectedText = in_input_text.Trim(_spaces);
                return;
            }

            int space_index = in_input_text.IndexOf(_space);
            if(space_index == -1)
            {
                out_parsed_command.CommandText = in_input_text;
                return;
            }

            out_parsed_command.CommandText = in_input_text.Substring(0, space_index);
            out_parsed_command.SelectedText = in_input_text.Substring(space_index).Trim(_spaces);
        }

        protected bool HasCommonStart(string input_command, string in_template)
        {
            if (input_command.Length > in_template.Length)
                return false;

            int len = Math.Min(input_command.Length, in_template.Length);
            for(int i = 0; i < len; i++)
            {
                if (input_command[i] != in_template[i])
                    return false;
            }
            return true;
        }
    }
}
