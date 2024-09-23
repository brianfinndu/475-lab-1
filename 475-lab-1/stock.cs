using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _475_lab_1
{
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent;

        // Name of stock
        private string _name;
        // Starting value of stock
        private int _initialValue;
        // Max change of stock that is possible
        private int _maxChange;
        // Threshold value where we notify subscribers to the event
        private int _threshold;
        // Number of changes the stock goes through
        private int _numChanges;
        // Current value fo the stock.
        private int _currentValue;

        private readonly Thread _thread;
        public string StockName { get => _name; set => _name = value; }
        public int InitalValue { get => _initialValue; set => _initialValue = value; }
        public int CurrentValue { get => _currentValue; set => _currentValue = value; }
        public int MaxChange { get => _maxChange; set => _maxChange = value; }
        public int Threshold { get => _threshold; set => _threshold = value; }
        public int NumChanges { get => _numChanges; set => _numChanges = value; }

        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            _name = name;
            _initialValue = startingValue;
            _currentValue = startingValue;
            _maxChange = maxChange;
            _threshold = threshold;
            _thread = new Thread(new ThreadStart(Activate));
            _thread.Start();
        }

        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500);
                ChangeStockValue();
            }
        }

        public void ChangeStockValue()
        {
            var rand = new Random();
            CurrentValue += rand.Next(1, MaxChange);
            NumChanges++;
            if ((CurrentValue - _initialValue) > Threshold)
            {
                StockEvent?.Invoke(this, new StockNotification(StockName, CurrentValue, NumChanges));
            }
        }
    }
}