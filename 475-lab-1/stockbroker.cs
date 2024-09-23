﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;

namespace _475_lab_1
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        readonly string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1_output.txt");
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";
        public static bool titlesWritten = false;
        public static bool titlesPrinted = false;

        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += Helper;
        }

        public void Helper(object Sender, StockNotification e)
        {
            Stock newStock = (Stock)Sender;
            string message = $"{BrokerName.PadRight(10)}{e.StockName.PadRight(15)}{e.CurrentValue.ToString().PadRight(10)}{e.NumChanges.ToString().PadRight(10)}{DateTime.Now.ToLongDateString()}" + " " + $"{DateTime.Now.ToLongTimeString()}";
            try
            {
                using (StreamWriter outputFile = new StreamWriter(destPath, true))
                {
                    if (!titlesWritten)
                    {
                        outputFile.Write(titles);
                        titlesWritten = true;
                    }
                    outputFile.WriteLine(message);
                }
                if (!titlesPrinted)
                {
                    Console.WriteLine(titles);
                    titlesPrinted = true;
                }
                Console.WriteLine(message);
            }
            catch (IOException O)
            {

            }
        }
    }
}