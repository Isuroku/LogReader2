using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogReader2
{
    public interface ICommandConsoleOwner
    {
        void CloseCommandConsole(LogFileControl in_log_control);
        void ExecuteCommandConsole(LogFileControl in_log_control, string in_command_text);

        IReadOnlyCollection<IConsoleCommand> GetCommands();

        IReadOnlyCollection<string> GetCommandHistory();
    }

    public partial class CommandConsole : UserControl
    {
        ICommandConsoleOwner _owner;
        LogFileControl _log_control;

        bool _hold_tips = false;

        bool _command_tips_or_history = true;

        public CommandConsole()
        {
            InitializeComponent();
        }

        public void Init(ICommandConsoleOwner in_owner, string in_start_text, string in_selected_text, LogFileControl in_log_control)
        {
            _owner = in_owner;
            _log_control = in_log_control;

            bool start_present = !string.IsNullOrEmpty(in_start_text);
            bool select_present = !string.IsNullOrEmpty(in_selected_text);

            var sb = new StringBuilder();
            if (start_present)
                sb.Append(in_start_text);

            if (select_present)
            {
                sb.Append(' ');
                sb.Append(in_selected_text);
            }

            tbCommand.Text = sb.ToString();

            tbCommand.SelectionStart = string.IsNullOrEmpty(in_start_text) ? 0 : in_start_text.Length;
        }

        private void CommandConsole_Load(object sender, EventArgs e)
        {
            tbCommand.Focus();
            tbCommand.DeselectAll();
        }

        private void tbCommand_TextChanged(object sender, EventArgs e)
        {
            if (_hold_tips)
                return;

            RefillHints();
        }

        private void RefillHints()
        {
            if (_hold_tips)
                return;

            if (_command_tips_or_history)
                CollectCommandHints();
            else
                CollectCommandHistory();
        }

        private void CollectCommandHints()
        {
            lbTips.Items.Clear();
            List<string> out_hints = new List<string>();
            foreach (IConsoleCommand cc in _owner.GetCommands())
            {
                cc.GetHints(tbCommand.Text, out_hints);
                foreach (string hint in out_hints)
                    lbTips.Items.Add(hint);
                out_hints.Clear();
            }
        }

        private void CollectCommandHistory()
        {
            lbTips.Items.Clear();
            IReadOnlyCollection<string> hist = _owner.GetCommandHistory();
            foreach (string h in hist)
            {
                lbTips.Items.Add(h);
            }
        }

        private void tbCommand_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void SelectNextHint(bool inUpOrDown)
        {
            if(lbTips.Items.Count == 0)
                return;

            _hold_tips = true;

            int index = lbTips.SelectedIndex;
            if (index == -1)
            {
                index = inUpOrDown ? lbTips.Items.Count - 1 : 0;
            }
            else
            {
                index = inUpOrDown ? index - 1 : index + 1;
                index = Math.Max(0, Math.Min(index, lbTips.Items.Count - 1));
            }

            lbTips.SelectedIndex = index;

            tbCommand.Text = lbTips.SelectedItem.ToString();
            int space_index = tbCommand.Text.IndexOf(' ');
            tbCommand.SelectionStart = space_index != -1 ? space_index : tbCommand.Text.Length;
            tbCommand.SelectionLength = 0;

            _hold_tips = false;
        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey: _command_tips_or_history = !_command_tips_or_history; RefillHints(); e.Handled = true; break;
                case Keys.Enter: _owner.ExecuteCommandConsole(_log_control, tbCommand.Text); e.Handled = true; break;
                case Keys.Escape: _owner.CloseCommandConsole(_log_control); e.Handled = true; break;
                case Keys.Down: SelectNextHint(false); e.Handled = true; break;
                case Keys.Up: SelectNextHint(true); e.Handled = true; break;
            }
        }
    }
}
