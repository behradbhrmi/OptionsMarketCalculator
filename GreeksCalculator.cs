using OptionsMarketTestTask.Models;
using MathNet.Numerics.Distributions;

namespace OptionsMarketTestTask;
public static class GreeksCalculator
{ 
    // GreeksCalculator class provides methods for calculating various option Greeks.
    // It uses the Black-Scholes option pricing model to compute these values based on European-style theoretical price.

    public static OptionGreeks CalculateOptionGreeks(
        double S0, // Current price of the underlying asset 
        double X, // Strike price of the option
        double t, // Time to expiration (in years)
        double σ, // Implied volatility (annualized)
        double r, // Risk-free interest rate (annualized)
        double q, // Dividend yield (annualized)
        double td = 365) //Number of days in a year (default is 365)
    {
        // Thrown when input parameters are invalid
        if (S0 <= 0 || X <= 0 || t <= 0 || σ <= 0 || r < 0 || q < 0)
        {
            throw new ArgumentException("Invalid input parameters.");
        }

        S0 = Convert.ToDouble(S0);
        X = Convert.ToDouble(X);
        σ = Convert.ToDouble(σ / 100);
        r = Convert.ToDouble(r / 100);
        q = Convert.ToDouble(q / 100);
        t = Convert.ToDouble(t / td);

        // Calculating d1 and d2 based on the formula 
        double d1 = (Math.Log(S0 / X) + (r - q + 0.5 * Math.Pow(σ, 2)) * t) / (σ * Math.Sqrt(t)); 
        double d2 = d1 - σ * Math.Sqrt(t);
        // object containing calculated option Greeks
        OptionGreeks greeks = new OptionGreeks();
        greeks.CallTheta = CalculateCallTheta(S0, X, σ, r, q, t, d1, d2, td);
        greeks.PutTheta = CalculatePutTheta(S0, X, σ, r, q, t, d1, d2, td);
        greeks.CallPremium = CalculateCallPremium(S0, X, σ, r, q, t, d1);
        greeks.PutPremium = CalculatePutPremium(S0, X, r, q, t, d1, d2);
        greeks.CallDelta = CalculateCallDelta(q, t, d1);
        greeks.PutDelta = CalculatePutDelta(q, t, d1);
        greeks.Gamma = CalculateGamma(S0, σ, t, d1);
        greeks.Vega = CalculateVega(S0, t, d1);
        greeks.CallRho = CalculateCallRho(X, t, r, d2);
        greeks.PutRho = CalculatePutRho(X, t, r, d2);

        return greeks;
    }
    // Calculate rach parameter based on its formula
    public static double CalculateCallTheta(double S0, double X, double σ, double r, double q, double t, double d1, double d2, double td)
    {
        return (-((S0 * σ * Math.Exp(-q * t)) / (2 * Math.Sqrt(t)) * (1 / (Math.Sqrt(2 * Math.PI))) * Math.Exp(-(d1 * d1) / 2))
                - (r * X * Math.Exp(-r * t) * Normal.CDF(0, 1, d2))
                + (q * Math.Exp(-q * t) * S0 * Normal.CDF(0, 1, d1))) / td;
    }

    public static double CalculatePutTheta(double S0, double X, double σ, double r, double q, double t, double d1, double d2, double td)
    {
        return (-((S0 * σ * Math.Exp(-q * t)) / (2 * Math.Sqrt(t)) * (1 / (Math.Sqrt(2 * Math.PI))) * Math.Exp(-(d1 * d1) / 2))
                + (r * X * Math.Exp(-r * t) * Normal.CDF(0, 1, -d2))
                - (q * Math.Exp(-q * t) * S0 * Normal.CDF(0, 1, -d1))) / td;
    }

    public static double CalculateCallPremium(double S0, double X, double σ, double r, double q, double t, double d1)
    {
        return Math.Exp(-q * t) * S0 * Normal.CDF(0, 1, d1) - X * Math.Exp(-r * t) * Normal.CDF(0, 1, d1 - σ * Math.Sqrt(t));
    }

    public static double CalculatePutPremium(double S0, double X, double r, double q, double t, double d1, double d2)
    {
        return X * Math.Exp(-r * t) * Normal.CDF(0, 1, -d2) - Math.Exp(-q * t) * S0 * Normal.CDF(0, 1, -d1);
    }

    public static double CalculateCallDelta(double q, double t, double d1)
    {
        return Math.Exp(-q * t) * Normal.CDF(0, 1, d1);
    }

    public static double CalculatePutDelta(double q, double t, double d1)
    {
        return Math.Exp(-q * t) * (Normal.CDF(0, 1, d1) - 1);
    }

    public static double CalculateGamma(double S0, double σ, double t, double d1)
    {
        return (Math.Exp(-d1 * d1 / 2) / (S0 * σ * Math.Sqrt(t))) * (1 / (Math.Sqrt(2 * Math.PI)));
    }

    public static double CalculateVega(double S0, double t, double d1)
    {
        return (S0 * Math.Exp(-d1 * d1 / 2) * Math.Sqrt(t)) * (1 / (Math.Sqrt(2 * Math.PI)));
    }

    public static double CalculateCallRho(double X, double t, double r, double d2)
    {
        return (X * t * Math.Exp(-r * t) * Normal.CDF(0, 1, d2)) / 100;
    }

    public static double CalculatePutRho(double X, double t, double r, double d2)
    {
        return (-X * t * Math.Exp(-r * t) * Normal.CDF(0, 1, -d2)) / 100;
    }
}
