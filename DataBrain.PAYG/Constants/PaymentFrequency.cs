using System.Runtime.Serialization;

namespace DataBrain.PAYG.Service.Constants;

/// <summary>
///     Payment Frequency
/// </summary>
public enum PaymentFrequency
{
    [EnumMember(Value = "Weekly")]
    Weekly = 1,
    [EnumMember(Value = "Fortnightly")]
    Fortnightly = 2,
    [EnumMember(Value = "Monthly")]
    Monthly = 3,
    [EnumMember(Value = "FourWeekly")]
    FourWeekly = 4
}