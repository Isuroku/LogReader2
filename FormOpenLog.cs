using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LogReader2
{
    public partial class FormOpenLog : Form, IPathControlOwner
    {
        private Form1 _owner;
        public FormOpenLog()
        {
            InitializeComponent();
        }

        public void SetOwner(Form1 in_owner) 
        { 
            _owner = in_owner; 

            foreach(string path in _owner.Settings.Folders)
                AddPath(path, String.Equals(_owner.Settings.LastSelectedFolders, path));
        }

        private void AddPath(string in_path, bool in_select = false)
        {
            var control = new PathControl();

            control.BorderStyle = BorderStyle.FixedSingle;

            control.SetPath(in_path, this);

            flowLayoutPanel1.Controls.Add(control);

            if (in_select)
            {
                ClearSelection();
                control.Select(true);
                OnControlSelected(control);
            }
        }

        private int GetPathControlWidth()
        {
            return flowLayoutPanel1.ClientSize.Width - flowLayoutPanel1.Margin.Horizontal;
        }

        private void FormOpenLog_Load(object sender, EventArgs e)
        {
           
        }

        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            int index = flowLayoutPanel1.Controls.GetChildIndex(e.Control);
            // Set the width of the first control added to the flow layout and the anchor of the rest
            if (index == 0)
            {
                Size sz = e.Control.Size;
                sz.Width = GetPathControlWidth();
                e.Control.Size = sz;
            }
            else
            {
                e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
                return;

            // Set size of the first control in the flow layout as the rest will anchor depending on its width.
            flowLayoutPanel1.Controls[0].Anchor = AnchorStyles.Left | AnchorStyles.Top;
            flowLayoutPanel1.Controls[0].Size = new Size(GetPathControlWidth(), flowLayoutPanel1.Controls[0].Height);
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
                return;
            
            // Set size of the first control in the flow layout as the rest will anchor depending on its width.
            flowLayoutPanel1.Controls[0].Size = new Size(GetPathControlWidth(), flowLayoutPanel1.Controls[0].Height);
        }

        public bool OnPushSelect(PathControl in_control, bool in_value)
        {
            int index = flowLayoutPanel1.Controls.GetChildIndex(in_control);
            if (index < 0)
                return false;

            foreach(PathControl control in flowLayoutPanel1.Controls)
            {
                if (control != in_control)
                    control.Select(false);
            }

            OnControlSelected(in_control);

            return true;
        }

        private void ClearSelection()
        {
            foreach (PathControl control in flowLayoutPanel1.Controls)
            {
                 control.Select(false);
            }
        }

        private void OnControlSelected(PathControl in_control)
        {
            _owner.Settings.LastSelectedFolders = in_control.GetPath();
            _owner.SaveSettings();

            CollectFiles(in_control.GetPath());
        }

        private PathControl GetSelectedPathControl()
        {
            foreach (PathControl control in flowLayoutPanel1.Controls)
            {
                if (control.IsSelected)
                    return control;
            }
            return null;
        }

        private void btnAddPath_Click(object sender, EventArgs e)
        {
            string start_path = Path.GetDirectoryName(Application.ExecutablePath);
            PathControl control = GetSelectedPathControl();
            if (control != null)
                start_path = control.GetPath();

            folderBrowserDialog1.SelectedPath = start_path;
            folderBrowserDialog1.ShowNewFolderButton = false;

            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                AddPath(folderBrowserDialog1.SelectedPath, true);

                _owner.Settings.Folders.Add(folderBrowserDialog1.SelectedPath);
                _owner.SaveSettings();
            }
        }

        private void btnDeletePath_Click(object sender, EventArgs e)
        {
            PathControl control = GetSelectedPathControl();
            if (control == null)
                return;

            _owner.Settings.Folders.Remove(control.GetPath());
            _owner.SaveSettings();

            flowLayoutPanel1.Controls.Remove(control);

            ClearSelection();

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                control = flowLayoutPanel1.Controls[0] as PathControl;
                control.Select(true);
                OnControlSelected(control);
            }
        }

        private void btnPathUp_Click(object sender, EventArgs e)
        {
            PathControl control = GetSelectedPathControl();
            if (control == null)
                return;

            int index = flowLayoutPanel1.Controls.GetChildIndex(control);
            if (index <= 0)
                return;

            index--;
            flowLayoutPanel1.Controls.SetChildIndex(control, index);
        }

        private void btnPathDown_Click(object sender, EventArgs e)
        {
            PathControl control = GetSelectedPathControl();
            if (control == null)
                return;

            int index = flowLayoutPanel1.Controls.GetChildIndex(control);
            if (index < 0 || index >= flowLayoutPanel1.Controls.Count - 1)
                return;

            index++;
            flowLayoutPanel1.Controls.SetChildIndex(control, index);
        }

        public void OnPathChanged(PathControl in_control, string in_path)
        {
            _owner.AddLogToConsole($"Path chaged: {in_path}", ELogLevel.Info);

            if(in_control.IsSelected)
            {
                CollectFiles(in_path);
            }
        }

        private void FormOpenLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            _owner.OpenLogClosed();
            _owner = null;
        }

        private bool CollectFiles(string in_path)
        {
            //lbFiles.BeginInvoke(new Action(() =>
            //{
            lbFiles.Items.Clear();

            if (!Directory.Exists(in_path))
                return false;

            var files = Directory.EnumerateFiles(in_path, "*.*", SearchOption.AllDirectories) .Where(s => s.EndsWith(".log") || s.EndsWith(".txt"));

            foreach (string fl in files)
            {
                lbFiles.Items.Add(new SLogFileInfo
                {
                    FullPath = fl,
                    Name = Path.GetFileName(fl)
                });
            }
            //}));

            return true;
        }

        private void lbFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFile();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            if (lbFiles.SelectedIndex == -1)
                return;

            SLogFileInfo sfi = (SLogFileInfo)lbFiles.SelectedItem;

            _owner.OpenLogFile(sfi.FullPath);
            Close();
        }
    }

    struct SLogFileInfo
    {
        public string FullPath;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
