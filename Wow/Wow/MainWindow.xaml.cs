﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Diagnostic expert system instance.
        /// </summary>
        public DiagnosticExpertSystem Expert { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Expert = new DiagnosticExpertSystem();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            //Configure open file dialog box.
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Выбор базы знаний",
                DefaultExt = ".xml",
                Filter = "XML-файл (.xml)|*.xml",
                CheckFileExists = true
            };

            //Process open file dialog.
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    Expert.LoadDatabase(dlg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Во время загрузки базы знаний произошла ошибка.", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
