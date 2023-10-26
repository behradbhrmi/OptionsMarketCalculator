using OptionsMarketTestTask.Models;
namespace OptionsMarketTestTask;
internal class Program
{
        static void Main(string[] args)
        {
            double underlyingPrice = 34950.60;
            double strikePrice = 35000.00;
            double volatility = 14.72;
            double riskPrice = 10;
            double dividendYield = 0.0;
            double timeToExpiration = 3;
            OptionGreeks result = GreeksCalculator.CalculateOptionGreeks(
                underlyingPrice, 
                strikePrice, 
                timeToExpiration, 
                volatility, 
                riskPrice, 
                dividendYield);
            Console.WriteLine(result.CallTheta);
            Console.WriteLine(result.PutTheta);
            Console.WriteLine(result.CallPremium);
            Console.WriteLine(result.PutPremium);
            Console.WriteLine(result.CallDelta);
            Console.WriteLine(result.PutDelta);
            Console.WriteLine(result.Gamma);
            Console.WriteLine(result.Vega);
            Console.WriteLine(result.CallRho);
            Console.WriteLine(result.PutRho);
        }
}

