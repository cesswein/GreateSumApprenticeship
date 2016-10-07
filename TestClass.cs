using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_Application.Test
{
    
    [TestFixture]
    public class TestCashDrawersForMakingChange
    {

        const decimal TwentyDollars = 20.00m;
        const decimal NineteenFifty = 19.50m;

        const decimal OneHundredDollars = 100.00m;
        const decimal ThreeDollarsAndTwentySixCents = 3.26m;

        static readonly Dictionary<string, decimal> OnePennyOnly = new Dictionary<string, decimal>
        {
            {"PENNY", 0.01m },
            {"NICKEL", 0.00m },
            {"DIME", 0.00m },
            {"QUARTER", 0.00m },
            {"ONE", 0.0m},
            {"FIVE", 0.00m },
            {"TEN",  0.00m },
            {"TWENTY", 0.00m },
            {"ONE HUNDRED", 0.00m }
        };
        
        static readonly Dictionary<string, decimal> FiftyPenniesOnly = new Dictionary<string, decimal>
        {
            {"PENNY", 0.50m },
            {"NICKEL", 0.00m },
            {"DIME", 0.00m },
            {"QUARTER", 0.00m },
            {"ONE", 0.0m},
            {"FIVE", 0.00m },
            {"TEN",  0.00m },
            {"TWENTY", 0.00m },
            {"ONE HUNDRED", 0.00m }
        };

        static readonly Dictionary<string, decimal> MultipleBillsAndCoins = new Dictionary<string, decimal>
        {
            {"PENNY", 1.01m },
            {"NICKEL", 2.05m },
            {"DIME", 3.10m },
            {"QUARTER", 4.25m },
            {"ONE", 90.0m},
            {"FIVE", 55.00m },
            {"TEN", 20.00m },
            {"TWENTY", 60.00m },
            {"ONE HUNDRED", 100.00m }
        };

        const string ClosedMessage = "Closed";
        const string InsufficientFundsMessage = "Insufficient Funds";
        const string TwoQuartersOfChange = "[[\"QUARTER\", 0.50]]";
        const string ChangeOfBillsAndCoins = "[[\"TWENTY\", 60.00], [\"TEN\", 20.00], [\"FIVE\", 15.00], [\"ONE\", 1.00], [\"QUARTER\", 0.50], [\"DIME\", 0.20], [\"PENNY\", 0.04]]";

        static object[] TestParameters = {
                                          new object[] {NineteenFifty, TwentyDollars, MultipleBillsAndCoins, TwoQuartersOfChange},  
                                          new object[] {NineteenFifty, TwentyDollars, OnePennyOnly, InsufficientFundsMessage},
                                          new object[] {NineteenFifty, TwentyDollars, FiftyPenniesOnly, ClosedMessage},
                                          new object[] {NineteenFifty, TwentyDollars, MultipleBillsAndCoins, TwoQuartersOfChange},
                                          new object[] {ThreeDollarsAndTwentySixCents, OneHundredDollars, MultipleBillsAndCoins, ChangeOfBillsAndCoins},
                                          new object[] {ThreeDollarsAndTwentySixCents, OneHundredDollars, FiftyPenniesOnly, InsufficientFundsMessage},
                                          new object[] {NineteenFifty, TwentyDollars, FiftyPenniesOnly, ClosedMessage},
                                         };

        [Test, TestCaseSource(nameof(TestParameters))]
        public void Test(decimal price, decimal cash, Dictionary<string,decimal> drawer, string expectedChange)
        {
            var actualChange = CashDrawer.DispenseChange(price, cash, drawer);

            Assert.That(expectedChange, Is.EqualTo(actualChange));
        }
    }
}