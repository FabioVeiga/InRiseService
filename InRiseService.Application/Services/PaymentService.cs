using InRiseService.Application.DTOs.PaymentDto;
using InRiseService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace InRiseService.Application.Services
{
    public class PaymentService(ILogger<PaymentService> logger) : IPaymentService
    {
        private readonly string _key = Environment.GetEnvironmentVariable("SkipeAppKey")!;
        private readonly ILogger<PaymentService> _logger = logger;

        public string CreateSessionStripe(PaymentProductsRequestDto paymentProductsRequestDto)
        {
            try
            {
                var lineItens = new List<SessionLineItemOptions>();
                foreach (var item in paymentProductsRequestDto.ProductDtos)
                {
                    lineItens.Add(new SessionLineItemOptions(){
                        PriceData = new SessionLineItemPriceDataOptions(){
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions(){
                                Name = item.Name,
                            },
                            UnitAmountDecimal = item.Price,
                        },
                        Quantity = item.Quantity
                    });
                }
                StripeConfiguration.ApiKey = _key;
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes =
                    [
                        "card",
                    ],
                    LineItems = lineItens,
                    Mode = "payment",
                    SuccessUrl = "https://your-success-url.com/success",
                    CancelUrl = "https://your-cancel-url.com/cancel",
                };

                var service = new SessionService();
                Session session = service.Create(options);

                Console.WriteLine("Session ID: " + session.Id);
                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{OrderStatusService}::{Get}] - Exception: {Ex}", nameof(PaymentService), nameof(CreateSessionStripe), ex);
                throw;
            }
        }
    }
}