using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Application.Dto;
using Braintree;

namespace Application.Main
{
    public class PaymentsApplication : IPaymentsApplication
    {
        private readonly BraintreeGateway _gateway;
        public PaymentsApplication(BraintreeGateway gateway)
        {
            _gateway = gateway;
        }
        public async Task<ResponseDto<string?>> GetClientToken()
        {
            var data = new ResponseDto<string?> {IsSuccess = true, Message = "Client Token", Response = "200" };
            try
            {
                var clientToken = await _gateway.ClientToken.GenerateAsync();
                if (clientToken == null)
                {
                    data.IsSuccess = false;
                    data.Message = "Client token no generado."
;               }
                data.Data = clientToken;
                return data;
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Message = "Error: " + ex.Message;
                data.Response = "500";
                return data;
            }
        }

        public async Task<ResponseDto<Result<Transaction>?>> Checkout(PaymentRequestDto paymentRequest)
        {
            var data = new ResponseDto<Result<Transaction>?> { IsSuccess = true , Message = "Transaction", Response = "200"};
            try
            {
                var request = new TransactionRequest
                {
                    Amount = paymentRequest.Amount,
                    PaymentMethodNonce = paymentRequest.Nonce,
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }
                };

                var result = await _gateway.Transaction.SaleAsync(request);

                if (result == null)
                {
                    data.IsSuccess = false;
                    data.Message = "Problema al realizar transacción";
                    data.Response = "400";
                }
                data.Data = result;
                return data;
            }
            catch (Exception ex)
            {
                data.IsSuccess = false;
                data.Message = "Error: " + ex.Message;
                data.Response = "500";
                return data;
            }
        }
    }
}