using System;
using System.Collections.Generic;

namespace RealWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            IBM ibm = new IBM("IBM", 50.00);
            ibm.Attach(new Investor("Frodo"));
            ibm.Attach(new Investor("Sam"));
            ibm.Attach(new Investor("Gandalf"));

            ibm.Price = 100.00;
            ibm.Price = 120.00;
        }
    }

    abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();

        public Stock(string symbol, double price)
        {
            _symbol = symbol;
            _price = price;
        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (IInvestor investor in _investors)
            {
                investor.Update(this);
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public string Symbol
        {
            get { return _symbol; }
        }
    }

    class IBM : Stock
    {
        public IBM(string symbol, double price) : base(symbol, price)
        {

        }
    }

    interface IInvestor
    {
        void Update(Stock stock);
    }

    class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;

        public Investor(string name)
        {
            _name = name;
        }

        public void Update(Stock stock)
        {
            Console.WriteLine($"Notified {_name} of {stock.Symbol}'s change to ${stock.Price}");
        }

        public Stock stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
    }
}
