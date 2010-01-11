﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;

namespace WcfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Observable.Context = SynchronizationContexts.CurrentDispatcher;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new DragDropWindow().Show();
        }
    }
}
