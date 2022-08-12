using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum ELogLevel
{
    Error,
    Warning,
    Info
}

namespace LogReader2
{
    public partial class Form1 : Form, CascadeParser.IParserOwner, CascadeParser.ILogPrinter, ILogFileControlOwner
    {
        private const string SettingFileName = "Settings.txt";
        private CSettings _settings;
        private bool _settings_loading = false;

        CascadeSerializer.CCascadeSerializer _serializer;

        public CSettings Settings => _settings;

        public Form1()
        {
            InitializeComponent();
            _serializer = new CascadeSerializer.CCascadeSerializer(this);

            LoadSettings();
        }

        #region LogWindow Output
        void AddLogToRichText(string inText, Color inClr)
        {
            if (m_uiLogLinesCount > 1000)
                ClearLog();

            int length = rtLog.TextLength;  // at end of text
            rtLog.AppendText(inText);
            rtLog.SelectionStart = length;
            rtLog.SelectionLength = inText.Length;
            rtLog.SelectionColor = inClr;
            rtLog.SelectionStart = rtLog.TextLength;
            rtLog.SelectionLength = 0;

            rtLog.ScrollToCaret();
        }

        void ClearLog()
        {
            rtLog.Text = string.Empty;
            m_uiLogLinesCount = 0;
        }

        uint m_uiLogLinesCount = 0;
        public void AddLogToConsole(string inText, Color inClr)
        {
            if (rtLog.IsDisposed)
                return;

            string sres = string.Format("{0}: {1}{2}", m_uiLogLinesCount.ToString(), inText, Environment.NewLine);
            rtLog.BeginInvoke(new Action<string>(s => AddLogToRichText(s, inClr)), sres);
            m_uiLogLinesCount++;
        }

        public void AddLogToConsole(string inText, ELogLevel inLogLevel)
        {
            if (rtLog.IsDisposed)
                return;

            string sres = string.Format("{0}: {1}{2}", m_uiLogLinesCount.ToString(), inText, Environment.NewLine);

            Color clr = Color.Black;
            switch (inLogLevel)
            {
                case ELogLevel.Info: clr = Color.Black; break;
                case ELogLevel.Warning: clr = Color.Brown; break;
                case ELogLevel.Error: clr = Color.Red; break;
            }

            //tbLog.BeginInvoke(new Action<string>(s => tbLog.AppendText(s)), sres);
            rtLog.BeginInvoke(new Action<string>(s => AddLogToRichText(s, clr)), sres);
            m_uiLogLinesCount++;
        }

        #endregion //LogWindow Output

        #region CascadeParser Interfaces
        public string GetTextFromFile(string inFileName, object inContextData)
        {
            throw new NotImplementedException();
        }

        public void LogError(string inText)
        {
            AddLogToConsole(inText, ELogLevel.Error);
        }

        public void LogWarning(string inText)
        {
            AddLogToConsole(inText, ELogLevel.Warning);
        }

        public void Trace(string inText)
        {
            AddLogToConsole(inText, ELogLevel.Info);
        }
        #endregion //CascadeParser Interfaces

        #region Settings
        public string GetSettingFilePath() { return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), SettingFileName); }

        private void LoadSettings()
        {
            _settings_loading = true;
            string setting_file = GetSettingFilePath();
            if (!File.Exists(setting_file))
            {
                _settings = new CSettings();

                _settings.LogSplitterDistance = splitContainerMainLog.SplitterDistance;

                SaveSettings();
            }
            else
            {
                using (StreamReader reader = File.OpenText(setting_file))
                {
                    string text = reader.ReadToEnd();
                    _settings = _serializer.Deserialize<CSettings>(text, this);

                    if(_settings.LogSplitterDistance > 0)
                        splitContainerMainLog.SplitterDistance = _settings.LogSplitterDistance;

                    if(_settings.LogHide)
                        splitContainerMainLog.Panel2.Hide();
                    else
                        splitContainerMainLog.Panel2.Show();
                }
            }
            _settings_loading = false;
        }
                

        public void SaveSettings()
        {
            string setting_file = GetSettingFilePath();
            using (StreamWriter writer = File.CreateText(setting_file))
            {
                string text = _serializer.SerializeToCascade(_settings, this);
                writer.Write(text);
            }
        }

        #endregion

        #region Log Window

        public void SetLogHide(bool inValue)
        {
            if (_settings.LogHide == inValue || _settings_loading)
                return;

            _settings.LogHide = inValue;
            SaveSettings();

            if (_settings.LogHide)
                splitContainerMainLog.Panel2.Hide();
            else
                splitContainerMainLog.Panel2.Show();
        }

        private void btnLogHide_Click(object sender, EventArgs e)
        {
            SetLogHide(true);
        }

        private void openLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogHide(false);
        }

        private void splitContainerMainLog_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (_settings_loading)
                return;

            _settings.LogSplitterDistance = e.SplitY;
            SaveSettings();
        }

        #endregion

        #region Open Log File
        private FormOpenLog _current_open_log_form;
        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_current_open_log_form != null)
                return;

            _current_open_log_form = new FormOpenLog();
            _current_open_log_form.SetOwner(this);
            _current_open_log_form.Show();
        }

        public void OpenLogClosed()
        {
            _current_open_log_form = null;
        }

        public void OpenLogFile(string in_full_path)
        {
            AddLogToConsole($"OpenLogFile: {in_full_path}", ELogLevel.Info);

            AddLogControl(in_full_path);
        }

        #endregion

        private const float _max_width_percent = 100f;
        private const float _min_width_percent = 0.01f;

        public float GetPercentByPixel() 
        {
            float full_percent = splitContainerMainLog.Panel1.Controls.Count * _max_width_percent;
            return full_percent / splitContainerMainLog.Panel1.ClientSize.Width;
        }

        private void AddLogControl(string in_path)
        {
            var control = new LogFileControl();

            control.BorderStyle = BorderStyle.FixedSingle;
            control.WidthPercent = _max_width_percent;
            control.LoadFile(in_path, this);

            splitContainerMainLog.Panel1.Controls.Add(control);
        }

        private void splitContainerMainLog_Panel1_Resize(object sender, EventArgs e)
        {
            LogPanelsResize();
        }

        private void LogPanelsResize()
        {
            if (splitContainerMainLog.Panel1.Controls.Count == 0)
                return;

            int x_space = splitContainerMainLog.Panel1.Margin.Left;
            int y_space = splitContainerMainLog.Panel1.Margin.Top;

            float full_percent = splitContainerMainLog.Panel1.Controls.Count * _max_width_percent;
                        
            int height = splitContainerMainLog.Panel1.ClientSize.Height - splitContainerMainLog.Panel1.Margin.Vertical;
            
            int x = x_space;
            foreach (LogFileControl control in splitContainerMainLog.Panel1.Controls)
            {
                int width = (int)(splitContainerMainLog.Panel1.ClientSize.Width * (control.WidthPercent / full_percent)) - x_space;

                Size sz = new Size(width, height);

                control.Size = sz;
                control.Location = new Point(x, y_space);
                x += sz.Width + x_space;
            }
        }

        bool CheckChangeControl(LogFileControl control, float in_value)
        {
            float full_percent = splitContainerMainLog.Panel1.Controls.Count * _max_width_percent;

            float new_width_perc = control.WidthPercent + in_value;
            float new_width_perc_rel = new_width_perc / full_percent;
            return new_width_perc_rel > _min_width_percent && new_width_perc_rel < 1 - _min_width_percent;
        }

        public void OnSplitMove(int in_pixels, LogFileControl control)
        {
            int chld_index = splitContainerMainLog.Panel1.Controls.GetChildIndex(control);
            if (chld_index == -1 || chld_index == splitContainerMainLog.Panel1.Controls.Count - 1)
                return;

            //AddLogToConsole($"OnSplitMove. Pixels {in_pixels}", ELogLevel.Info);

            float perc = GetPercentByPixel() * in_pixels;
            if (!CheckChangeControl(control, perc))
                return;

            LogFileControl next_control = splitContainerMainLog.Panel1.Controls[chld_index + 1] as LogFileControl;
            if (!CheckChangeControl(next_control, -perc))
                return;

            control.WidthPercent += perc;
            next_control.WidthPercent -= perc;

            LogPanelsResize();
        }

        private void splitContainerMainLog_Panel1_ControlAdded(object sender, ControlEventArgs e)
        {
            LogPanelsResize();
        }
        
        public void CloseLogFileControl(LogFileControl control)
        {
            splitContainerMainLog.Panel1.Controls.Remove(control);
            LogPanelsResize();
        }
    }
}
