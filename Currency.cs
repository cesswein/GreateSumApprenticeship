using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NUnit_Application.Test

{
    public class Currency
    {
        public decimal Value { get; set; }
        public string Name { get; set; }
        public Currency(){ }        
        public Currency(decimal value, string name)
        {
            Value = value;
            Name = name;
        }

        public Currency CreateNew()
        {
            return new Currency(Value, Name);
        }

        public Currency Change(decimal changeToBeTendered)
        {
            var numberOfBillsOrCoins = Math.Floor(changeToBeTendered/Value);
            var change = numberOfBillsOrCoins * Value;
            Value = change;

            return this;
        }

        public Currency TenderedFromDrawer(Dictionary<string, decimal> cashInDrawer)
        {
            var availableChangeFromDrawer = cashInDrawer[Name];
            var changeThatCanBeTendered = Math.Min(Value, availableChangeFromDrawer);
            Value = changeThatCanBeTendered;

            return this;
        }

        public bool CanBeDispensed()
        {
            return Value > 0.0m;
        }

        public string DisplayFormat()
        {
            return $"[\"{this.Name}\", {$"{this.Value:0.00}"}]";
        }   
    }
}