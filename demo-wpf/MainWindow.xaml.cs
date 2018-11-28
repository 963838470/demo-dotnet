﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo_wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string key = "test";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("触发了一个主线UI线程异常！");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { throw new InvalidOperationException("Something has gone wrong."); }));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() => { throw new InvalidOperationException("Something has gone wrong."); });
            t.IsBackground = true;
            t.Start();
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key]))
            {
                config.AppSettings.Settings[key].Value = txtWrite.Text;
            }
            else
            {
                config.AppSettings.Settings.Add(key, txtWrite.Text);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            txtRead.Text = ConfigurationManager.AppSettings[key];
        }
    }
}
