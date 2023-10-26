namespace OptionsMarketTestTask.Models;

// OptionGreeks class represents a container for storing various option Greeks values.
public class OptionGreeks
{
    public double CallTheta { get; set; }
    public double PutTheta { get; set; }
    public double CallPremium { get; set; }
    public double PutPremium { get; set; }
    public double CallDelta { get; set; }
    public double PutDelta { get; set; }
    public double Gamma { get; set; }
    public double Vega { get; set; }
    public double CallRho { get; set; }
    public double PutRho { get; set; }
}
