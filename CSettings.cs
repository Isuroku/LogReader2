

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LogReader2
{
    public class CSettings
    {
        public List<string> Folders;
        public string LastSelectedFolders;

        public List<string> Commands;

        public List<string> OpenHistory;

        public int LogSplitterDistance;
        public bool LogHide;

        private void OnDeserializedMethod(StreamingContext context)
        {
            if(Folders == null)
                Folders = new List<string>();
            if (Commands == null)
                Commands = new List<string>();
            if (OpenHistory == null)
                OpenHistory = new List<string>();
        }

    }
}
