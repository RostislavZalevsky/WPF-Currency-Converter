// <copyright file="MainWindow.xaml.cs" company="Black Sea National University">
// Copyright (c) Black Sea National University. All rights reserved.
// </copyright>
// <author>Rostislav Zalevsky</author>

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using CurrencyConverter.Data;
using CurrencyConverter.Model;

namespace CurrencyConverter
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Data

        private Timer _timer;
        private bool IsInit;
        public ExchangeRatesModel ExchangeRateL =>
            ExchangeRatesData.ExchangeRates.Any() &&
            ExchangeRatesData.ExchangeRates.Count > ComboBoxCurrenciesL.SelectedIndex && ComboBoxCurrenciesL.SelectedIndex >= 0 &&
            ExchangeRatesData.ExchangeRates.ElementAt(ComboBoxCurrenciesL.SelectedIndex).rates.Any()
                ? ExchangeRatesData.ExchangeRates.ElementAt(ComboBoxCurrenciesL.SelectedIndex)
                : new ExchangeRatesModel();
        public KeyValuePair<string, double> ExchangeRateR =>
            ExchangeRateL.rates.Any() &&
            ExchangeRateL.rates.Count > ComboBoxCurrenciesR.SelectedIndex && ComboBoxCurrenciesR.SelectedIndex >= 0 &&
            ExchangeRateL.rates.Any(p => p.Key == ComboBoxCurrenciesR.SelectedValue.ToString().ToUpper())
                ? ExchangeRateL.rates.Single(p => p.Key == ComboBoxCurrenciesR.SelectedValue.ToString().ToUpper())
                : new KeyValuePair<string, double>("", 0);

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            _timer = new Timer(_ => OnCallBack(), null, 0, Timeout.Infinite);
        }

        private void OnCallBack()
        {
            _timer.Dispose();
            ExchangeRatesData.UpdateCurrencyRate();

            if (!IsInit)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsInit = true;
                    ComboBoxCurrenciesL.ItemsSource = ExchangeRatesData.ExchangeRates;
                    ComboBoxCurrenciesL.SelectedIndex =
                        ExchangeRatesData.ExchangeRates.IndexOf(
                            ExchangeRatesData.ExchangeRates.Single(p => p.@base == "EUR"));

                    ComboBoxCurrenciesL.SelectionChanged += CalculateL;
                    AmountL.TextChanged += CalculateL;

                    ComboBoxCurrenciesR.ItemsSource = ExchangeRatesData.ExchangeRates
                        .ElementAt(ComboBoxCurrenciesL.SelectedIndex).rates.Keys;
                    ComboBoxCurrenciesR.SelectedIndex =
                        ExchangeRatesData.ExchangeRates.IndexOf(
                            ExchangeRatesData.ExchangeRates.Single(p => p.@base == "USD"));

                    ComboBoxCurrenciesR.SelectionChanged += CalculateR;
                    AmountR.TextChanged += CalculateR;

                    AmountL.Text = "1000";
                });
            _timer = new Timer(_ => OnCallBack(), null, 1000 * 2, Timeout.Infinite); //in 10 seconds
        }

        public void Calculate(bool left = true)
        {
            Application.Current.Dispatcher.Invoke(() => // optional
            {
                Rate.Content = (string.IsNullOrEmpty(ExchangeRateL.@base) ? "---" : ExchangeRateL.@base) + "/" + (string.IsNullOrEmpty(ExchangeRateR.Key) ? "---" : ExchangeRateR.Key);
                Price.Content = ExchangeRateR.Value > 0 ? $"{ExchangeRateR.Value}" : "-";

                switch (left)
                {
                    default:
                    {
                        AmountR.TextChanged -= CalculateR;
                        AmountR.Text =
                            ExchangeRateR.Value > 0 && !string.IsNullOrEmpty(ExchangeRateL.@base) &&
                            double.TryParse(AmountL.Text, out var a)
                                ? $"{a * ExchangeRateR.Value}"
                                : string.Empty;
                        AmountR.TextChanged += CalculateR;
                    }
                        break;
                    case false:
                    {
                        AmountL.TextChanged -= CalculateL;
                        AmountL.Text =
                            ExchangeRateR.Value > 0 && !string.IsNullOrEmpty(ExchangeRateR.Key) &&
                            double.TryParse(AmountR.Text, out var a)
                                ? $"{a / ExchangeRateR.Value}"
                                : string.Empty;
                        AmountL.TextChanged += CalculateL;
                    }
                        break;
                }
            });
        }

        #region Events

        private void CalculateL(object sender, TextChangedEventArgs e) => Calculate();

        private void CalculateR(object sender, TextChangedEventArgs e) => Calculate(left: false);

        private void CalculateL(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxCurrenciesR.SelectionChanged -= CalculateR;
            var temp = ExchangeRateR.Key;
            ComboBoxCurrenciesR.ItemsSource = ExchangeRateL.rates.Keys;
            ComboBoxCurrenciesR.SelectedValue = temp;
            if (ComboBoxCurrenciesR.SelectedValue == null)
            {
                ComboBoxCurrenciesR.SelectedIndex = 0;
            }
            ComboBoxCurrenciesR.SelectionChanged += CalculateR;
            Calculate();
        }

        private void CalculateR(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxCurrenciesL.SelectionChanged -= CalculateL;
            var temp = ExchangeRateL.@base;
            ComboBoxCurrenciesL.ItemsSource = ExchangeRatesData.ExchangeRates;
            ComboBoxCurrenciesL.SelectedIndex = ExchangeRatesData.ExchangeRates.Any(p => p.@base == temp) ?
                ExchangeRatesData.ExchangeRates.IndexOf(
                    ExchangeRatesData.ExchangeRates.Single(p => p.@base == temp )) : 0;

            ComboBoxCurrenciesL.SelectionChanged += CalculateL;
            Calculate( /*left: false*/);
        }

        private void ClearAmountL(object sender, RoutedEventArgs e) => AmountL.Text = string.Empty;

        private void ClearAmountR(object sender, RoutedEventArgs e) => AmountR.Text = string.Empty;

        private void Calculate(object sender, RoutedEventArgs e) => Calculate();

        private void MenuItem_OnClick(object sender, RoutedEventArgs e) => this.Close();

        #endregion
    }
}