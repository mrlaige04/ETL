﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Classes
{
    internal class AppManager
    {
        private FileManager fileManager;
        public bool isEnabled { get; set; } = false;
        public AppManager()
        {
            fileManager = new();
        }

        public void Start() {           
            fileManager.Start();
            isEnabled = true;
        }

        public void Stop()
        {            
            fileManager.Stop();
            isEnabled = false;
        }
    }
}
