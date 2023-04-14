using DataBrain.PAYG.Service.Constants;

namespace DataBrain.PAYG.Service.Services;

public interface IPAYGService
{
    float GetTax(float earnings, PaymentFrequency frequency);
}