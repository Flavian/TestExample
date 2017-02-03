using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : INotifyPropertyChanged
    {
        #region Data

        private double _data;
        public double Data
        {
            get{ return _data; }
            set	{ _data = value; OnPropertyChanged(); }
        }

        #endregion

        #region Datas

        private List<double> _datas;
        public List<double> Datas
        {
            get { return _datas; }
            set { _datas = value; OnPropertyChanged("Datas"); }
        }

        #endregion

        #region TargetData

        private double _targetData;
        public double TargetData
        {
            get{ return _targetData; }
            set	{ _targetData = value; OnPropertyChanged("TargetData"); }
        }

        #endregion

        public MainWindow()
        {
            Datas = new List<double> {1.1325,5.159841891841,6168.1978489498498};
            Data = 3.123456789;
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            double value= 3.15864198498;

            Thread thread = new Thread(() =>
            {
                try
                {
                    Clipboard.SetText(value.ToString(CultureInfo.InvariantCulture));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
           
        }
    }
}
