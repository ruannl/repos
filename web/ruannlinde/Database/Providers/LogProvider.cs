using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using Ruann.Linde.Database.Models;
using Ruann.Linde.Extensions;

namespace Ruann.Linde.Database.Providers {
	using StackifyLib;

	public interface ILogProvider {

		/// <summary>
		/// Gets the log file content
		/// </summary>
		/// <returns></returns>
		//List<Log> GetLog(DateTime? from, DateTime? to);
		bool ClearLog();
	}

	public class LogProvider : ILogProvider {
		public string LogFileDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rl.log");
		private static readonly ILog Log = LogManager.GetLogger(typeof(LogProvider).Name);
		private static ApplicationDatabaseContext _applicationDatabaseContext;

		public LogProvider(ApplicationDatabaseContext applicationDatabaseContext) {
			_applicationDatabaseContext = applicationDatabaseContext;
		}

		

		///// <summary>
		///// Gets the log file content
		///// </summary>
		///// <returns></returns>
		//public List<Log> GetLog(DateTime? from, DateTime? to) {
		//	try {
		//		var logData = new List<Log>();
				

		//		//Retrieve Maximum Date
		//		//var MaxDate = (from d in dataRows select d.Date).Max();
		//		foreach (var log in _applicationDatabaseContext.Log.Where(l => l.Date >= minDate && l.Date <= maxDate)) logData.Add(log);

		//		return logData;

		//		#region log file appender
		//		//string logData = "";

		//		//var appenders = LogManager.GetRepository().GetAppenders();
		//		//foreach (var appender in appenders) {
		//		//	Debug.WriteLine(appender.Name);
		//		//	Debug.WriteLine(appender.GetType().Name);
		//		//}

		//		//Log.Info("GetLog");

		//		//Log.Info("Testing information log");
		//		//Log.Debug("Testing Debug log");
		//		//Log.Fatal("Testing Fatal log");
		//		//foreach (var appender in appenders)


		//		//	if (appender is RollingFileAppender rollingFileAppender) {
		//		//		rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
		//		//		rollingFileAppender.ActivateOptions();
		//		//	}

		//		//using (var fs = new FileStream(LogFileDirectory, FileMode.Open, FileAccess.Read)) {
		//		//	using (var ms = new MemoryStream()) {
		//		//		fs.CopyTo(ms);
		//		//		ms.Seek(0, SeekOrigin.Begin);
		//		//		using (var reader = new StreamReader(ms, Encoding.UTF8)) {
		//		//			logData = reader.ReadToEnd();
		//		//		}
		//		//	}
		//		//}
		//		////var lines = logData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
		//		//logData = logData.Replace(Environment.NewLine, "<br />"); 
		//		#endregion
		//	}
		//	catch (Exception e) {
		//		throw new DomainException(e.Message);
		//	}
		//}

		public bool ClearLog() {
			try {
				//var appenders = LogManager.GetRepository().GetAppenders();
				//foreach (var appender in appenders)
				//	if (appender is RollingFileAppender rollingFileAppender) {
				//		rollingFileAppender.ImmediateFlush = true;
				//		rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
				//		rollingFileAppender.ActivateOptions();
				//	}

				//File.WriteAllText(LogFileDirectory, string.Empty);
				var logRange = _applicationDatabaseContext.Log.Select(e => e).AsEnumerable();
				_applicationDatabaseContext.Log.RemoveRange(logRange);
				_applicationDatabaseContext.SaveChanges();

				Log.Info("Log Cleared",null);

				return true;
			}
			catch (Exception e) {
				throw new DomainException(e.Message);
			}
		}
	}
}