using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LogReader2
{

    public interface ILogFileControlOwner
    {
        void OnSplitMove(int in_pixels, LogFileControl control);
        void CloseLogFileControl(LogFileControl control);
        void StartCommand(LogFileControl control, string in_input_text, string in_selected_text);
        void OnFocusEnterLogFileControl(LogFileControl control);
    }

    public partial class LogFileControl : UserControl
    {

        private ILogFileControlOwner _owner;

        public float WidthPercent = 0;

        private bool _load_text;

        public LogFileControl()
        {
            InitializeComponent();

            tbLogText.BookmarkColor = Color.Black;
        }

        public bool LoadFile(string in_path, ILogFileControlOwner in_owner)
        {
            _owner = in_owner;
            labelFileName.Text = in_path;

            return LoadText();
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

        private bool LoadText()
        {
            string fp = labelFileName.Text;
            if (!File.Exists(fp))
                return false;
            
            string text = File.ReadAllText(fp, Encoding.UTF8);
            //string source_text = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _load_text = true;
            tbLogText.AppendText(text);
            _load_text = false;

            tbLogText.Focus();

            return true;
        }

        private void tbLogText_TextChanging(object sender, FastColoredTextBoxNS.TextChangingEventArgs e)
        {
            if (_load_text)
                return;

            e.Cancel = true;

            _owner.StartCommand(this, e.InsertingText, tbLogText.SelectedText);
        }

        public void ReturnFocus()
        {
            tbLogText.Focus();
        }

        private void tbLogText_Enter(object sender, EventArgs e)
        {
            _owner.OnFocusEnterLogFileControl(this);
        }

        EllipseStyle _ellipse_style = new EllipseStyle();

        public bool SelectText(string in_selected_text)
        {
            if (string.IsNullOrEmpty(in_selected_text))
                return false;

            tbLogText.Bookmarks.Clear();

            List<int> lines = tbLogText.FindLines(in_selected_text, RegexOptions.IgnoreCase);
            foreach (int ln in lines)
                tbLogText.BookmarkLine(ln);

            tbLogText.Range.ClearStyle(_ellipse_style);
            tbLogText.Range.SetStyle(_ellipse_style, in_selected_text, RegexOptions.IgnoreCase);

            return true;
        }

        public bool HideLineText(string in_selected_text)
        {
            if (string.IsNullOrEmpty(in_selected_text))
                return false;

            string marker = $"Hide Line Text: {in_selected_text}";

            List<int> lines = tbLogText.FindLines(in_selected_text, RegexOptions.IgnoreCase);
            if (lines.Count == 0)
                return false;

            int start_line = lines[0];
            tbLogText[start_line].FoldingStartMarker = marker;

            int end_line = start_line;
            for(int i =1; i < lines.Count; i++)
            {
                int ln = lines[i];
                int dist = ln - end_line;
                if(dist > 1)
                {
                    tbLogText[end_line].FoldingEndMarker = marker;

                    start_line = ln;
                    tbLogText[start_line].FoldingStartMarker = marker;
                }

                end_line = ln;
            }

            tbLogText[end_line].FoldingEndMarker = marker;

            tbLogText.CollapseAllFoldingBlocks();

            return true;
        }
    }

    class EllipseStyle : Style
    {
        public override void Draw(Graphics gr, Point position, Range range)
        {
            //get size of rectangle
            Size size = GetSizeOfRange(range);
            //create rectangle
            Rectangle rect = new Rectangle(position, size);
            //inflate it
            rect.Inflate(2, 2);
            //get rounded rectangle
            var path = GetRoundedRectangle(rect, 7);
            //draw rounded rectangle
            gr.DrawPath(Pens.Red, path);
        }
    }
}
