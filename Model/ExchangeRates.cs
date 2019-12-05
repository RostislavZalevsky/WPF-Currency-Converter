// <copyright file="ExchangeRates.cs" company="Black Sea National University">
// Copyright (c) Black Sea National University. All rights reserved.
// </copyright>
// <author>Rostislav Zalevsky</author>

namespace CurrencyConverter.Model
{
    using System;
    using System.Collections.Generic;

    public class ExchangeRatesModel
    {
        public string @base { get; set; } = "";
        public Dictionary<string, double> rates { get; set; } = new Dictionary<string, double>();
        public DateTime date { get; set; } = DateTime.MinValue;
        public string error { get; set; } = "";
    }
}