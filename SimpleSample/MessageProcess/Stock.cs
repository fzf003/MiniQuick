using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleSample.MessageProcess
{
    public class Stock
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public string Volume { get; set; }

        public static IEnumerable<Stock> LoadQuotes()
        {
           
            return LoadQuotes( Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
        }

        static IEnumerable<Stock> LoadQuotes(string path)
        {
            return from filepath in Directory.EnumerateFiles(path, "*.csv")
                   let symbol = Path.GetFileNameWithoutExtension(filepath)
                   from quote in LoadStock(symbol, filepath)
                   where quote.Volume.Length > 0
                   select quote;
        }

        static IEnumerable<Stock> LoadStock(string symbol, string path)
        {
            using (var reader = File.OpenText(path))
            {
                reader.ReadLine();
                var prevQuote = default(Stock);
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    var elements = line.Split(',');

                    var date = DateTime.ParseExact(elements[0], "d-MMM-yy", CultureInfo.InvariantCulture);
                    var open = decimal.Parse(elements[1]);
                    var high = decimal.Parse(elements[2]);
                    var low = decimal.Parse(elements[3]);
                    var close = decimal.Parse(elements[4]);
                    var volume = elements[5];

                    var quote = new Stock
                    {
                        Symbol = symbol,
                        Date = date,
                        Close = close,
                        High = high,
                        Low = low,
                        Open = open,
                        Volume = volume
                    };

                    yield return quote;

                    prevQuote = quote;
                }
            }
        }
    }
}
