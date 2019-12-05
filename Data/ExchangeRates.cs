// <copyright file="ExchangeRates.cs" company="Black Sea National University">
// Copyright (c) Black Sea National University. All rights reserved.
// </copyright>
// <author>Rostislav Zalevsky</author>

namespace CurrencyConverter.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using Model;
    using Newtonsoft.Json;

    public static class ExchangeRatesData
    {
        public static List<ExchangeRatesModel> ExchangeRates { get; set; } = new List<ExchangeRatesModel>();

        #region UpdateCurrencyRate

        public static void UpdateCurrencyRate()
        {
            var exchangeRates = Get();
            if (!string.IsNullOrEmpty(exchangeRates.error)) return;
            ExchangeRates = ExchangeRates
                .Where(p => exchangeRates.@base == p.@base || exchangeRates.rates.ContainsKey(p.@base)).ToList();

            foreach (var rate in exchangeRates.rates)
            {
                Set(Get(rate.Key));
            }
        }

        #endregion

        #region GetAPI
        private static ExchangeRatesModel Get(string @base = "USD")
        {
            var exchangeRates = new ExchangeRatesModel();
            var request = WebRequest.Create("https://api.exchangeratesapi.io/latest?base=" + @base.ToUpper());
            var response = request.GetResponse();
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                if ((line = stream.ReadLine()) != null)
                {
                    exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesModel>(line);
                }
            }

            return exchangeRates;
        }

        #endregion

        #region SetExchangeRates

        private static void Set(ExchangeRatesModel exchangeRates)
        {
            if (!string.IsNullOrEmpty(exchangeRates.error)) return;
            if (ExchangeRates.Any(p => p.@base == exchangeRates.@base))
            {
                var exchangeRatesData = ExchangeRates.FirstOrDefault(p => p.@base == exchangeRates.@base);
                exchangeRatesData.rates = exchangeRates.rates;
                exchangeRatesData.date = exchangeRates.date;
            }
            else
            {
                ExchangeRates.Add(exchangeRates);
            }
        }

        #endregion
    }
}
