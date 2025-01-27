using InRiseService.Application.DTOs.PaymentDto;

namespace InRiseService.Application.Interfaces
{
    public interface IPaymentService
    {
        string CreateSessionStripe(PaymentProductsRequestDto paymentProductsRequestDto);
    }
}