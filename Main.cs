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
			HttpServer httpServer = new HttpServer(Properties.Settings.Default.HTTPPort, Properties.Settings.Default.HTTPIP, Properties.Settings.Default.WebPath, ConsoleOutputLogger);
			Thread http_server_thread = new Thread(new ThreadStart(httpServer.listen));
			http_server_thread.Start();
			#endregion
		}
	}
}
