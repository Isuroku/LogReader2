using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogReader2
{

    public interface ILogFileControlOwner
    {
        void OnSplitMove(int in_pixels, LogFileControl control);
        void CloseLogFileControl(LogFileControl control);
    }

    public partial class LogFileControl : UserControl
    {

        private ILogFileControlOwner _owner;

        public float WidthPercent = 0;

        public LogFileControl()
        {
            InitializeComponent();
        }

        public void LoadFile(string in_path, ILogFileControlOwner in_owner)
        {
            _owner = in_owner;
            labelFileName.Text = in_path;
            tbLogText.Text = in_path;
        }

        int _last_mouse;

        private void panelSplit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Cursor.Current = Cursors.SizeWE;

            panelSplit.Capture = true;

            Point screen_point = panelSplit.PointToScreen(e.Location);
            _last_mouse = screen_point.X;
        }

        private void panelSplit_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Cursor.Current = Cursors.VSplit;
            panelSplit.Capture = false;
        }

        private void panelSplit_MouseMove(object sender, MouseEventArgs e)
        {
            if (!panelSplit.Capture)
                return;

            Point screen_point = panelSplit.PointToScreen(e.Location);

            int move_pixels = screen_point.X - _last_mouse;
            _last_mouse = screen_point.X;

            _owner.OnSplitMove(move_pixels, this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _owner.CloseLogFileControl(this);
        }
    }
}
