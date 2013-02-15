using System;
using System.Threading;
using HTTP;
using System.IO;
using System.Collections.Generic;

namespace GalleryServer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			#region ConsoleOutputLogger
			ConsoleOutputLogger ConsoleOutputLogger = new ConsoleOutputLogger();
			ConsoleOutputLogger.verbose = true;
			ConsoleOutputLogger.writeLogfile = false;
			#endregion

			#region Logo
			#endregion

			ConsoleOutputLogger.writeLogfile = true;

			#region Start HTTP Server
			HttpServer httpServer = new HttpServer(8080, "127.0.0.1", "./web", ConsoleOutputLogger);
			Thread http_server_thread = new Thread(new ThreadStart(httpServer.listen));
			http_server_thread.Start();
			#endregion
		}
	}
}
