﻿using RentACar5.Commons.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar5.Commons.Concrete.Loggers
{
    internal class DBLogger : LogBase
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                //Code to log data to the database
            }
        }
    }
}
