using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogReader2
{
    public interface IPathControlOwner
    {
        bool OnPushSelect(PathControl in_control, bool in_value);
        void OnPathChanged(PathControl in_control, string in_path);
    }

    public partial class PathControl : UserControl
    {
        private IPathControlOwner _owner;
        
        public bool IsSelected { get; private set; }

        public PathControl()
        {
            InitializeComponent();
        }

        public void SetPath(string in_path, IPathControlOwner in_owner)
        {
            _owner = in_owner;
            tbPath.Text = in_path;
        }

        public string GetPath() { return tbPath.Text; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsSelected)
                return;

            if (_owner.OnPushSelect(this, true))
                Select(true);
        }

        public void Select(bool in_value)
        {
            IsSelected = in_value;

            BackColor = in_value ? Color.FromKnownColor(KnownColor.ActiveCaption) : Color.FromKnownColor(KnownColor.Control);
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            _owner.OnPathChanged(this, tbPath.Text);
        }
    }
}
