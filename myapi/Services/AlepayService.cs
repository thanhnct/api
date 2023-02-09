using myapi.DTOs;
using myapi.Models;
using NETCore.Encrypt;
using Newtonsoft.Json;
using System.Text;

namespace myapi.Services
{
    public class AlepayService
    {
        private readonly AlePayConfig alePayConfig = new AlePayConfig();
        private readonly IConfiguration _configuration;

        public AlepayService(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration.GetSection("AlePay").Bind(alePayConfig);
        }
        public async Task<string> Checkout(AlePayRequestPaymentDto model)
        {
            try
            {
                var url = "https://alepay-v3.nganluong.vn/api/v3/checkout/request-payment";

                string _tokenKey = alePayConfig.TokenKey,
                    _orderCode = "Bikelife_001",
                    _customMerchantId = "lephuong",
                    _currency = "VND",
                    _orderDescription = "Thông Tin Mô Tả Hóa Đơn",
                    _returnUrl = alePayConfig.Host + "/return",
                    _cancelUrl = alePayConfig.Host + "/cancel",
                    _buyerName = "Lê Thị Trúc Phương",
                    _buyerEmail = "phuong.le@bizmac.com.vn", _buyerPhone = "0969710301",
                    _buyerAddress = "Đinh Bộ Lĩnh,_P.26,_Q. Bình Thạnh", _buyerCity = "Ho Chi Minh", _buyerCountry = "Viet Nam",
                    _signature = "";

                int _totalItem = 2;
                string _data =
                    "amount=" + model.amount +
                    "&buyerAddress=" + _buyerAddress +
                    "&buyerCity=" + _buyerCity +
                    "&buyerCountry=" + _buyerCountry +
                    "&buyerEmail=" + _buyerEmail +
                    "&buyerName=" + _buyerName +
                    "&buyerPhone=" + _buyerPhone +
                    "&cancelUrl=" + _cancelUrl +
                    "&currency=" + _currency +
                    "&customMerchantId=" + _customMerchantId +
                    "&orderCode=" + _orderCode +
                    "&orderDescription=" + _orderDescription +
                    "&returnUrl=" + _returnUrl +
                    "&tokenKey=" + _tokenKey +
                    "&totalItem=" + _totalItem;
                _signature = EncryptProvider.HMACSHA256(_data, alePayConfig.ChecksumKey).ToLower();

                string json = JsonConvert.SerializeObject(new
                {
                    amount = model.amount,
                    buyerAddress = _buyerAddress,
                    buyerCity = _buyerCity,
                    buyerCountry = _buyerCountry,
                    buyerEmail = _buyerEmail,
                    buyerName = _buyerName,
                    buyerPhone = _buyerPhone,
                    cancelUrl = _cancelUrl,
                    currency = _currency,
                    customMerchantId = _customMerchantId,
                    orderCode = _orderCode,
                    orderDescription = _orderDescription,
                    returnUrl = _returnUrl,
                    tokenKey = _tokenKey,
                    totalItem = _totalItem,
                    signature = _signature
                });

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
