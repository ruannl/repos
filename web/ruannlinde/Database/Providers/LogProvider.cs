using System;
using System.IO;
using System.Text;
using log4net;
using log4net.Appender;

namespace Ruann.Linde.Database.Providers {
	public interface ILogProvider {
		string LogFileDirectory { get; }

		/// <summary>
		/// Gets the log file content
		/// </summary>
		/// <returns></returns>
		string GetLog();
		bool ClearLog();
	}

	public class LogProvider : ILogProvider {
		public string LogFileDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rl.log");
		private static readonly ILog Log = LogManager.GetLogger(typeof(LogProvider).Name);

		/// <summary>
		/// Gets the log file content
		/// </summary>
		/// <returns></returns>
		public string GetLog() {
			try {
				string logData;

				var appenders = LogManager.GetRepository().GetAppenders();

				foreach (var appender in appenders)
					if (appender is RollingFileAppender rollingFileAppender) {
						rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
						rollingFileAppender.ActivateOptions();
					}

				using (var fs = new FileStream(LogFileDirectory, FileMode.Open, FileAccess.Read)) {
					using (var ms = new MemoryStream()) {
						fs.CopyTo(ms);
						ms.Seek(0, SeekOrigin.Begin);
						using (var reader = new StreamReader(ms, Encoding.UTF8)) {
							logData = reader.ReadToEnd();
						}
					}
				}

				logData = logData.Replace(Environment.NewLine, "<br />");
				return logData;
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
		public bool ClearLog() {
			try {
				var appenders = LogManager.GetRepository().GetAppenders();
				foreach (var appender in appenders)
					if (appender is RollingFileAppender rollingFileAppender) {
						rollingFileAppender.ImmediateFlush = true;
						rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
						rollingFileAppender.ActivateOptions();
					}

				File.WriteAllText(LogFileDirectory, string.Empty);
				Log.Info("Log Cleared");

				return true;
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}
	}
}