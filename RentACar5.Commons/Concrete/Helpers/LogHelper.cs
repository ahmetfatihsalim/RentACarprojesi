﻿using RentACar5.Commons.Abstract;
using RentACar5.Commons.Concrete.Loggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar5.Commons.Concrete.Helpers
{
    public static class LogHelper
    {
        private static LogBase _logger = null;
        private static bool _enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"]);
        public static void Log(LogTarget target, string message, bool isError = false)
        {
            if (_enableLogging)
            {
                switch (target)
                {
                    case LogTarget.File:
                        _logger = new FileLogger();
                        _logger.Log(message, isError);
                        break;
                    case LogTarget.Database:
                        _logger = new DBLogger();
                        _logger.Log(message, isError);
                        break;
                    case LogTarget.EventLog:
                        _logger = new EventLogger();
                        _logger.Log(message, isError);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
