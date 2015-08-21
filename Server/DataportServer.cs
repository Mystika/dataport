using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dataport.Server
{
    public class DataportServer
    {
        HttpListener listener;
        Dictionary<string,string> dict = new Dictionary<string, string>();

        bool _IsRunning = false;
        int _Port = 8080;

        public bool IsRunning
        {
            get { return _IsRunning; }
        }

        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        public bool AddFile(string fileName, string filePath)
        {
            if (!dict.ContainsKey(fileName) && !dict.ContainsValue(filePath))
            {
                dict.Add(fileName, filePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RenameFile(string oldName, string newName)
        {
            if (dict.ContainsKey(oldName))
            {
                string val = dict[oldName];
                dict.Remove(oldName);
                dict.Add(newName, val);
                return true;
            }
            return false;
        }

        public bool RemoveFile(string fileName)
        {
            if (dict.ContainsKey(fileName))
            {
                dict.Remove(fileName);
                return true;
            }
            return false;
        }

        public void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Clear();
            listener.Prefixes.Add(String.Format("http://+:{0}/", _Port));
            listener.Start();
            listener.BeginGetContext(GetCallback, null);
            _IsRunning = true;
        }

        public void Stop()
        {
            _IsRunning = false;
            listener.Abort();
        }


        private void GetCallback(IAsyncResult ar)
        {
            if (_IsRunning)
            {
                
                listener.BeginGetContext(GetCallback, null);

                HttpListenerContext req = listener.EndGetContext(ar);
                switch (req.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        handleGET(req);
                        break;
                    case "POST":
                        //handlePOST(req);
                        break;
                    default:
                        return;
                }
                req.Response.Close();
            }
        }

        private void handleGET(HttpListenerContext req)
        {
            

            string reqfile = req.Request.Url.AbsolutePath;
            

            if(reqfile == "/favicon.ico")
            {
                return;
            }

            ((frmMain)Application.OpenForms[0]).AddLog(String.Format("GET {0} : {1}", req.Request.RemoteEndPoint, reqfile));
            if (reqfile == "" || reqfile == "/")
            {
                StreamReader reader = new StreamReader(@".\Template\index.html");
                string html = parseTemplate(reader.ReadToEnd());
                reader.Close();
                byte[] bytes = Encoding.UTF8.GetBytes(html);

                req.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
            else
            {

                reqfile = reqfile.Remove(0,1);
                if (reqfile.IndexOf('/') == -1)
                {
                    if (!dict.ContainsKey(reqfile))
                    {
                        req.Response.OutputStream.Write(Encoding.UTF8.GetBytes("404-1"), 0, Encoding.UTF8.GetBytes("404-1").Length);
                        return;
                    }
                    else if (File.Exists(dict[reqfile]))
                    {
                        sendFile(req, dict[reqfile]);
                    }
                    else if (Directory.Exists(dict[reqfile]))
                    {
                        string[] files = Directory.GetDirectories(dict[reqfile]).Concat(Directory.GetFiles(dict[reqfile])).ToArray();

                        StreamReader reader = new StreamReader(@".\Template\index.html");
                        string html = parseTemplate(reader.ReadToEnd(), reqfile, files);
                        reader.Close();
                        byte[] bytes = Encoding.UTF8.GetBytes(html);

                        req.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else { return; }
                }
                else if(dict.ContainsKey(reqfile.Substring(0, reqfile.IndexOf('/'))))
                {
                    string root = reqfile.Substring(0, reqfile.IndexOf('/'));
                    string subUrl = reqfile.Substring(reqfile.IndexOf('/'),reqfile.Length - reqfile.IndexOf('/')).Remove(0,1).Replace('/','\\');
                    string localPath = Path.Combine(dict[root], subUrl);
                    Console.WriteLine(localPath);
                    if (dict.ContainsKey(root)) {
                        if (!File.Exists(localPath) && !Directory.Exists(localPath))
                        {
                            req.Response.OutputStream.Write(Encoding.UTF8.GetBytes("404-2"), 0, Encoding.UTF8.GetBytes("404-2").Length);
                            return;
                        }
                        else if (File.Exists(localPath))
                        {
                            sendFile(req, localPath);
                        }
                        else if (Directory.Exists(localPath))
                        {
                            string[] files = Directory.GetDirectories(localPath).Concat(Directory.GetFiles(localPath)).ToArray();

                            StreamReader reader = new StreamReader(@".\Template\index.html");
                            string html = parseTemplate(reader.ReadToEnd(), reqfile, files);
                            reader.Close();
                            byte[] bytes = Encoding.UTF8.GetBytes(html);

                            req.Response.OutputStream.Write(bytes, 0, bytes.Length);
                        }
                        else { return; }
                    }
                    else
                    {
                        req.Response.OutputStream.Write(Encoding.UTF8.GetBytes("404-3"), 0, Encoding.UTF8.GetBytes("404-3").Length);
                    }
                }

            }
        }


        private void sendFile(HttpListenerContext req, string path)
        {
            var response = req.Response;
            
            using (FileStream fs = File.OpenRead(path))
            {
                string filename = dict.FirstOrDefault(x => x.Value == path).Key;

                response.ContentLength64 = fs.Length;
                response.SendChunked = false;
                response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                response.AddHeader("Content-disposition", "attachment; filename=" + filename);

                byte[] buffer = new byte[64 * 1024];
                int read;
                using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
                {
                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, read);
                        bw.Flush();
                    }

                    bw.Close();
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";
                response.OutputStream.Close();
            }
        }


        private string parseTemplate(string html)
        {
            int tagStart = html.IndexOf("{{");
            int tagEnd = html.IndexOf("}}", tagStart);
            string tag = html.Substring( tagStart, tagEnd-tagStart+2);
            string tagName = tag.Remove(tag.Length - 2, 2).Replace(" ", "").Remove(0, 2).ToLower();

            StringBuilder sb = new StringBuilder();
            if(tagName == "filelist")
            {
                if (dict.Keys.Count > 0)
                {
                    foreach (string key in dict.Keys)
                        sb.Append(String.Format("<a href='./{0}'>{0}</a><br/>", key));
                }
                else
                {
                    sb.Append("there is no shared files");
                }
            }

            
            return html.Replace(tag, sb.ToString());
        }

        private string parseTemplate(string html, string parentUrl ,string[] filelist)
        {
            int tagStart = html.IndexOf("{{");
            int tagEnd = html.IndexOf("}}", tagStart);
            string tag = html.Substring(tagStart, tagEnd - tagStart + 2);
            string tagName = tag.Remove(tag.Length - 2, 2).Replace(" ", "").Remove(0, 2).ToLower();

            string root = "";
            if (parentUrl.LastIndexOf('/') != -1) root = parentUrl.Substring(0, parentUrl.LastIndexOf('/'));

            StringBuilder sb = new StringBuilder(String.Format("<a href='/{0}'>..</a><br/>", root));
            if (tagName == "filelist")
            {
                foreach (string file in filelist)
                {
                    string uri = (parentUrl + "/" + file.Substring(file.LastIndexOf('\\') + 1)).Replace("//", "/");
                    sb.Append(String.Format("<a href='/{0}'>{1}</a><br/>", uri,file.Substring(file.LastIndexOf('\\') + 1)));
                }
            }

            return html.Replace(tag, sb.ToString());
        }
    }
}