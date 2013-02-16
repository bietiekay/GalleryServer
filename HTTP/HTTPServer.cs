using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using GalleryServer;

namespace HTTP
{
    #region HttpServer
    /// <summary>
    /// Implements a HTTP Server Listener
    /// </summary>
    public class HttpServer
    {
        #region Data
        private string docRoot;
        private Int32 HTTPServer_Port;
        private String HTTPServer_ListeningIP;
        private String HTTPServer_DocumentRoot;
        private ConsoleOutputLogger ConsoleOutputLogger;
        private ThumbnailCache ThumbCache;
        #endregion

        #region Construction
		public HttpServer(Int32 HTTP_Port, String HTTP_ListeningIP, String HTTP_DocumentRoot, ConsoleOutputLogger Logger)
        {
            HTTPServer_Port = HTTP_Port;
            HTTPServer_ListeningIP = HTTP_ListeningIP;
            HTTPServer_DocumentRoot = HTTP_DocumentRoot;
            ConsoleOutputLogger = Logger;
            ThumbCache = new ThumbnailCache(GalleryServer.Properties.Settings.Default.CacheImages);
        }
        #endregion

        #region Listener
        /// <summary>
        /// Create a new server socket, set up all the endpoints, bind the socket and then listen
        /// </summary>
        public void listen()
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddress = IPAddress.Parse(HTTPServer_ListeningIP);
            IPEndPoint endpoint = new IPEndPoint(ipaddress, HTTPServer_Port);

            try
            {
                // Create a new server socket, set up all the endpoints, bind the socket and then listen
                listener.Bind(endpoint);
                listener.Blocking = true;
                listener.Listen(-1);
                ConsoleOutputLogger.WriteLineToScreenOnly("[HTTP] Administrationsoberfläche unter http://" + HTTPServer_ListeningIP+ ":" + HTTPServer_Port+ " erreichbar.");
                while (true)
                {
                    try
                    {
                        // Accept a new connection from the net, blocking till one comes in
                        Socket s = listener.Accept();

                        // Create a new processor for this request
                        HttpProcessor processor = new HttpProcessor(s, HTTPServer_DocumentRoot, ConsoleOutputLogger, ThumbCache);


                        // Dispatch that processor in its own thread
                        Thread thread = new Thread(new ThreadStart(processor.process));
                        thread.Start();
                        //Thread.Sleep(10);
                        //processor.process();

                    }
                    catch (NullReferenceException)
                    {
                        // Don't even ask me why they throw this exception when this happens
                        ConsoleOutputLogger.WriteLine("[FEHLER@HTTP] Kann nicht auf TCP-Port " + HTTPServer_Port+ " verbinden - wird vermutlich schon benutzt.");
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleOutputLogger.WriteLine("[FEHLER@HTTP] " + e.Message);
            }
        }
        #endregion
    }
    #endregion
}
