using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WGET.Classes
{
    class HTTP_Client
    {
        private string Host;
        private string Object_Path;
        private List<byte> All_Data;
        private List<string> Response_Header;

        private string filetype;
        private string httpClientStatus;
        private string NewLocation;
        string[] extensions = { ".png", ".pdf", ".jpg", ".ppt", ".txt", ".doc" };

        //HTTP_Client Constructor
        //Get URL and Check if it is Valid then Start Downloading Process
        public HTTP_Client(string url)
        {
            Uri main_url;
            if (!Uri.TryCreate(url, UriKind.Absolute, out main_url))
            {
                httpClientStatus = "Invalid URL";
                return;
            }

            Host = main_url.Host;
            Object_Path = main_url.PathAndQuery;
            filetype = Path.GetExtension(url);
            All_Data = new List<byte>();
            Response_Header = new List<string>();
            if (!Parsing())
            {
                return;
            }

            while (true)
            {
                GetNewUrl();
                if (NewLocation == "")
                {
                    break;
                }

                main_url = new Uri(NewLocation);
                Host = main_url.Host;
                Object_Path = main_url.PathAndQuery;
                All_Data = new List<Byte>();
                Response_Header = new List<string>();
                if (!Parsing())
                {
                    return;
                }
            }
        }

        //Calling Get Header Function and Check Status Code 
        private bool Parsing()
        {
            if (SocketConnection(Host, Object_Path))
            {
                GetHeader(All_Data);
                Check();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Header from the HTTP Response (Cinvert it to String)
        private void GetHeader(List<byte> All_Data_Copy)
        {
            String Line = "";
            int charsCounter = 0;
            foreach (byte B in All_Data_Copy)
            {
                charsCounter++;
                Char c = (char)B;
                if (c == '\n')
                {
                    if (Line.Equals("\r"))
                        break;
                    Response_Header.Add(Line);
                    Line = "";
                }
                else
                    Line += c;
            }
        }

        //Remove The Header from the Response
        private void CutHeader()
        {
            String Line = "";
            int BytesCounter = 0;
            foreach (byte B in All_Data)
            {
                BytesCounter++;
                Char c = (char)B;
                if (c == '\n')
                {
                    if (Line == "\r")
                        break;
                    Line = "";
                }
                else
                    Line += c;
            }
            All_Data.RemoveRange(0, BytesCounter);
        }

        //calling "Writing File on The Local Disk" Process
        public void Writing_Data(string FolderPath)
        {
            if (extensions.Contains(filetype))
            {

                goto Writing;
            }
            else
            {
                filetype = ".html";
            }


        Writing:

            CutHeader();
      
            File_Manager FM = new File_Manager(FolderPath, filetype);

            FM.Write_File(All_Data);
        }


        //Check Response Status Code
        private void Check()
        {
            if (Response_Header[0].Contains("404 Not Found"))
            {
                httpClientStatus = "404 Not Found";
            }
            else if (Response_Header[0].Contains("400 Bad Request"))
            {
                httpClientStatus = "400 Bad Request";
            }
            else if (Response_Header[0].Contains("301 Moved Permanently"))
            {
                GetNewUrl();
            }
            else
            {
                httpClientStatus = "200 OK";
                return;
            }

        }

        //Get the Status Code 
        public string GetStatus()
        {
            return httpClientStatus;
        }

        //Extract The New Location URL from The Response
        private void GetNewUrl()
        {
            NewLocation = "";
            foreach (string S in Response_Header)
            {
                if (S.StartsWith("Location: "))
                {
                    NewLocation = S;
                    NewLocation = NewLocation.Remove(0, 10);
                }
            }
        }

        //Send The Request to Server and Establish The Connection with it
        public bool SocketConnection(string server, string Object_Path)
        {
            string http_request =
                "GET /"
                + Object_Path
                + " HTTP/1.0\r\n"
                + "Host: "
                + server
                + "\r\nConnection: Close\r\n\r\n";

            Byte[] bytesSent = Encoding.ASCII.GetBytes(http_request);
            Byte[] bytesReceived = new Byte[1024];

            IPAddress[] ip_addresses;

            try
            {
                ip_addresses = Dns.GetHostAddresses(server);

            }
            catch
            {
                httpClientStatus = "Connection Error";
                All_Data.Clear();
                return false;
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ip_addresses, 80);

            if (!socket.Connected)
            {
                All_Data.Clear();
                return false;
            }

            socket.Send(bytesSent, bytesSent.Length, 0);

            int bytes = 0;

            do
            {
                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                for (int i = 0; i < bytes; i++)
                    All_Data.Add(bytesReceived[i]);
            }
            while (bytes > 0);

            return true;
        }
    }
}
