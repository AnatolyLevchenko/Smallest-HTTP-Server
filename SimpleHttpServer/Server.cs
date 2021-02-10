using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SimpleHttpServer
{
    class Server
    {
        private bool started = false;
        public List<string> GetHosts()
        {
            var ls = new List<string> { "127.0.0.1" };

            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    ls.Add(endPoint.Address.ToString());
                }
            }
            catch (Exception)
            {

            }

            return ls;

        }


        HttpListener listener = new HttpListener();


        public bool Start(string ip, int port)
        {
            if (started)
            {
                Stop();
                return false;
            }

            listener.Prefixes.Add($"http://{ip}:{port}/");
            listener.Start();

            started = true;

            listener.BeginGetContext(Callback, null);

            return true;
        }


        public void Stop()
        {
            listener.Stop();
            started = false;
            listener = new HttpListener();
        }

        // copy to output directory foreach file
        private void Callback(IAsyncResult ar)
        {
            if (started == false)
                return;

            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(Callback, null);


            HttpListenerResponse response = context.Response;


            if (context.Request.RawUrl == "/" || context.Request.RawUrl.EndsWith(".html"))
            {
                response.Headers.Set("Content-Type", "text/html");
            }
            else if (context.Request.RawUrl.EndsWith(".css"))
            {
                response.Headers.Set("Content-Type", "text/css");
            }

            else if (context.Request.RawUrl.EndsWith(".jpg"))
            {
                response.Headers.Set("Content-Type", "img/jpeg");
            }

            else if (context.Request.RawUrl.EndsWith(".js"))
            {
                response.Headers.Set("Content-Type", "text/javascript");
            }



            try
            {
                var buffer = File.ReadAllBytes(context.Request.RawUrl == "/" ? Path.Combine("./html/", "index.html") :
                    Path.Combine("./html/", "." + context.Request.RawUrl));


                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.OutputStream.Close();
            }
            catch (FileNotFoundException e)
            {
                response.StatusCode = 404;
                context.Response.OutputStream.Close();

            }

        }
    }
}
