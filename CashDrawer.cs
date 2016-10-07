using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;

namespace NUnit_Application.Test
{
    public static class CashDrawer
    { 
            private static Currency[] USCurrency =
{
            new Currency {Value =  100m, Name = "ONE HUNDRED"},
            new Currency {Value =  20m, Name = "TWENTY"},
            new Currency {Value =  10m, Name = "TEN"},
            new Currency {Value =  5m, Name = "FIVE"},
            new Currency {Value =  1m, Name = "ONE"},
            new Currency {Value =  0.25m, Name = "QUARTER"},
            new Currency {Value =  0.10m, Name = "DIME"},
            new Currency {Value =  0.05m, Name = "NICKEL"},
            new Currency {Value =  0.01m, Name = "PENNY"},
        };
    public static readonly Dictionary<string, decimal> CashInDrawerTest1 = new Dictionary<string, decimal>
        {
            {"PENNY", 1.01m },
            {"NICKEL", 2.05m },
            {"DIME", 3.10m },
            {"QUARTER", 0.25m },
            {"ONE", 90.0m},
            {"FIVE", 55.00m },
            {"TEN", 20.00m },
            {"TWENTY", 60.00m },
            {"ONE HUNDRED", 100.00m }
        };


    public static string DispenseChange(decimal price, decimal cash, Dictionary<string, decimal> cashInDrawer)
    {
        var changeToBeTendered = cash - price;
        var totalInDrawer = cashInDrawer.Sum(curr => curr.Value);

        if (JustEnoughChangeInDrawer(changeToBeTendered, totalInDrawer))
        {
            return "Closed";
        }

        var displayChange = "[";

        foreach (var curr in USCurrency)
        {
            var changeFromDrawer = curr.CreateNew()
                                       .Change(changeToBeTendered)
                                       .TenderedFromDrawer(cashInDrawer);

            if (changeFromDrawer.CanBeDispensed())
            {
                changeToBeTendered -= changeFromDrawer.Value;
                displayChange += changeFromDrawer.DisplayFormat() + ", ";
            }
        }

        if (NotEnoughChangeInDrawer(changeToBeTendered))
        {
            return "Insufficient Funds";
        }

        var correctedEndOfDisplay = displayChange.TrimEnd(' ').TrimEnd(',') + "]";

        return correctedEndOfDisplay;
    }

    private static bool JustEnoughChangeInDrawer(decimal changeToBeTendered, decimal totalInDrawer)
    {
        return changeToBeTendered == totalInDrawer;
    }

    private static bool NotEnoughChangeInDrawer(decimal changeToBeTendered)
    {
        return changeToBeTendered > 0.0m;
    }
}
}
