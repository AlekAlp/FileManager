using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    [Serializable]
    class stringElement
    {
        public string titleElement { get; set; }
        public string sizeElement { get; set; }
        public string timeCreate { get; set; }
        public string timeChange { get; set; }
        public string fileAddress { get; set; }
        public string countFiles { get; set; }
        public bool hiddenFile { get; set; }
        public stringElement(string TitleElement,                              
                             string TimeCreate,
                             string TimeChange,
                             string CountFiles,
                             string SizeElement,
                             string FileAddress,
                             bool HiddenFile)
        {
            TitleElement = titleElement;
            SizeElement = sizeElement;
            TimeCreate = timeCreate;
            TimeChange = timeChange;
            CountFiles = countFiles;
            FileAddress = fileAddress;
            HiddenFile = hiddenFile;
        }
        public stringElement()
        { }
    }
}
