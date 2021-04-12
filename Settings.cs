using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    [Serializable]
    class Settings
    {
        public string copyCommand { get; set; }
        public string openCommand { get; set; }
        public string upDirCommand { get; set; }
        public string nextCommand { get; set; }
        public string backCommand { get; set; }
        public string deleteCommand { get; set; }
        public string exitCommand { get; set; }
        public int sizePage { get; set; }
        public string lastPath { get; set; }
        public bool showHidden { get; set; }
        public Settings(string CopyCommand,
                             string OpenCommand,
                             string UpDirCommand,
                             string NextCommand,
                             string BackCommand,
                             string DeleteCommand,
                             string ExitCommand,
                             int SizePage,
                             string LastPath,
                             bool ShowHidden)
        {
            CopyCommand = copyCommand;
            OpenCommand = openCommand;
            UpDirCommand = upDirCommand;
            NextCommand = nextCommand;
            BackCommand = backCommand;
            DeleteCommand = deleteCommand;
            ExitCommand = exitCommand;

            SizePage = sizePage;

            LastPath = lastPath;
            ShowHidden = showHidden;
        }
        public Settings()
        { }
    }
}
