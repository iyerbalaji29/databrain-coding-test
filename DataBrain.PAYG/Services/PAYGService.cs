using DataBrain.PAYG.Service.Constants;

namespace DataBrain.PAYG.Service.Services;

public record TaxTableItem(float X, float A, float B);

/// <summary>
/// Service that can compute the PAYG payable for a given amount over a given period.
/// Based on the ATO guidance 
/// https://www.ato.gov.au/uploadedFiles/Content/MEI/downloads/calculating-amounts-to-be-withheld-from-13-October-2020.pdf
/// </summary>
public class PAYGService : IPAYGService
{
    private readonly List<TaxTableItem> _taxTable = new()
    {
        new TaxTableItem(359f, 0f, 0f),
        new TaxTableItem(438f, 0.1900f, 68.3462f),
        new TaxTableItem(548f, 0.2900f, 112.1942f),
        new TaxTableItem(721f, 0.2100f, 68.3465f),
        new TaxTableItem(865f, 0.2190f, 74.8369f),
        new TaxTableItem(1282f, 0.3477f, 186.2119f),
        new TaxTableItem(2307f, 0.3450f, 182.7504f),
        new TaxTableItem(3461f, 0.3900f, 286.5965f),
        new TaxTableItem(3461f, 0.4700f, 563.5196f)
    };

    /// <summary>
    /// Gets the PAYG tax payable for the given taxable income earned over the period defined by frequency
    /// </summary>
    /// <param name="taxableIncome">The taxable income for the period</param>
    /// <param name="frequency">The length of time earnings were earned in</param>
    /// <returns>The PAYG payable, this includes the tax and medicare levi</returns>
    /// <exception cref="ArgumentException"></exception>
    public float GetTax(float taxableIncome, PaymentFrequency frequency)
    {
        return frequency switch
        {
            PaymentFrequency.Fortnightly => GetTaxFromTable(taxableIncome / 2) * 2,
            PaymentFrequency.FourWeekly => GetTaxFromTable(taxableIncome / 4) * 4,
            PaymentFrequency.Monthly => (float) Math.Round(GetTaxFromTable(GetMonthlyEarnings(taxableIncome)) * 13 / 3),
            PaymentFrequency.Weekly => GetTaxFromTable(taxableIncome),
            _ => throw new ArgumentException("frequency is invalid")
        };
    }

    private float GetTaxFromTable(float earnings)
    {
        earnings += 0.99f;
        var tableItem = _taxTable.OrderBy(a => a.X).FirstOrDefault(item => earnings < item.X) ?? _taxTable[^1];
        return (float) Math.Round(earnings * tableItem.A - tableItem.B);
    }

    private static float GetMonthlyEarnings(float earnings)
    {
        if (earnings - Math.Floor(earnings) == 0.33)
        {
            earnings += 0.01f;
        }
        return (float)Math.Floor(earnings * 3 / 13);
    }
}
