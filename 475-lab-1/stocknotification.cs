using System;
using System.Collections.Generic;
using System.Text;

namespace _475_lab_1
{
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }

        public StockNotification(string stockName, int currentValue, int numChanges)
        {
            this.StockName = stockName;
            this.CurrentValue = currentValue;
            this.NumChanges = numChanges;
        }
    }
}