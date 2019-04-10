using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGET.Classes
{
    //File Management Class
    class File_Manager
    {
        string Folder_Path;
        String File_Path;
        string File_Type;

        //File Manager Constructor
        public File_Manager(string Folder_Path, string File_Type)
        {
            this.Folder_Path = Folder_Path+"\\";
            File_Path = "";
            this.File_Type = File_Type;

        }

        //Generate Name to The File Requested
        public void SetFileName()
        {
            for (int i = 1; ; i++)
            {
                if (!File.Exists(Folder_Path + "File" + i.ToString() + File_Type))
                {
                    File_Path = Folder_Path + "File" + i.ToString() + File_Type;
                    break;
                }
            }

        }

        //Writing File On The Selected Folder
        public void Write_File(List<byte> Data)
        {
            SetFileName();
            FileStream Fs = new FileStream(File_Path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            foreach (byte B in Data)
            {
                Fs.WriteByte((byte)B);
            }
            Fs.Close();
        }
    }
}
