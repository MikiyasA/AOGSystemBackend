using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Attachments
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public long Size { get; private set; }
        public string Type { get; private set; }

        public void SetFileName(string fileName) {  FileName = fileName; }
        public void SetFilePath(string filePath) {  FilePath = filePath; }
        public void SetSize(long size) {  Size = size; }
        public void SetType(string type) {  Type = type; }


        public Attachment() { }
        public Attachment(string fileName, string filePath, long size, string type) 
        {
            FileName = fileName; 
            FilePath = filePath;
            Size = size;
            Type = type;
        }
    }
}
