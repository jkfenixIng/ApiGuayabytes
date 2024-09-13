using Application.Dto;
using Application.DTO;
using Braintree;

namespace Application.Interfaces
{
    public interface IPaymentsApplication
    {
        Task<ResponseDto<string?>> GetClientToken();
        Task<ResponseDto<Result<Transaction>?>> Checkout(PaymentRequestDto paymentRequest);
    }
}