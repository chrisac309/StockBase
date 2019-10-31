using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using SerilogWeb.Classic;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using static System.Environment;

namespace StockBase
{
    public static class LoggerConfig
    {
#pragma warning disable RECS0154 // Parameter is never used
        public static void Configure(HttpConfiguration config)
#pragma warning restore RECS0154 // Parameter is never used
        {
            // Use Seriog for logging
            // More information can be found here https://github.com/serilog/serilog/wiki/Getting-Started
            var basedir = AppDomain.CurrentDomain.BaseDirectory;

            // By default log file is located in 'C:\Users\<username>\AppData\Roaming\Logs' folder and named as the current assembly name
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(basedir + "/Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .ReadFrom.AppSettings()

                // Enrich with SerilogWeb.Classic (https://github.com/serilog-web/classic)
                .Enrich.WithHttpRequestUrl()
                .Enrich.WithHttpRequestType()

                .Enrich.WithExceptionDetails()

                .CreateLogger();

            // By defaut we don't want to see all HTTP requests in log file, but you can change this by ajusting this setting
            // Additional information can be found here https://github.com/serilog-web/classic
            SerilogWebClassic.Configure(cfg => cfg
              .LogAtLevel(LogEventLevel.Debug));

            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLogger());
        }
    }
}
