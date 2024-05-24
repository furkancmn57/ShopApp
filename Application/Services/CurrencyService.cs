using Application.Common.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class CurrencyService
    {
        string url = string.Format("https://www.tcmb.gov.tr/kurlar/today.xml");

        HttpClient client = new HttpClient();

        public async Task<List<Currency>> GetCurrency()
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string xml = await response.Content.ReadAsStringAsync();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);


            List<Currency> currencies = new List<Currency>();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                Currency currency = new Currency();
                currency.Code = node.Attributes["CurrencyCode"].Value;
                currency.CurrencyName = node["Isim"].InnerText;
                currency.Unit = Convert.ToInt32(node["Unit"].InnerText);
                currency.ForexBuying = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                currency.ForexSelling = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                currency.BanknoteBuying = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                currency.BanknoteSelling = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));

                currencies.Add(currency);
            }

            if (currencies.Count == 0)
            {
                throw new BusinessException("Kurlar alınamadı.");
            }

            return currencies;
        }
    }
}
