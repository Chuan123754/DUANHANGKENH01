using appAPI.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace appAPI.VnPay
{
    public class VnPayLibrary
    {
        //private readonly SortedList<string, string> _requestData = new SortedList<string, string>();
        //private readonly SortedList<string, string> _responseData = new SortedList<string, string>();

        //public void AddRequestData(string key, string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        _requestData.Add(key, value);
        //    }
        //}

        //public string CreateRequestUrl(string baseUrl, string hashSecret)
        //{
        //    var data = new StringBuilder();
        //    foreach (var (key, value) in _requestData)
        //    {
        //        data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        //    }

        //    string queryString = data.ToString().TrimEnd('&');
        //    string secureHash = HmacSha512(hashSecret, queryString);
        //    return $"{baseUrl}?{queryString}&vnp_SecureHash={secureHash}";
        //}

        //public bool ValidateSignature(string inputHash, string secretKey)
        //{
        //    var rawData = GetResponseData();
        //    var computedHash = HmacSha512(secretKey, rawData);
        //    return string.Equals(inputHash, computedHash, StringComparison.OrdinalIgnoreCase);
        //}

        //public void AddResponseData(string key, string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        _responseData.Add(key, value);
        //    }
        //}

        //public string GetResponseData(string key)
        //{
        //    return _responseData.TryGetValue(key, out var value) ? value : string.Empty;
        //}

        //private string GetResponseData()
        //{
        //    var data = new StringBuilder();
        //    foreach (var (key, value) in _responseData)
        //    {
        //        if (key != "vnp_SecureHash" && key != "vnp_SecureHashType")
        //        {
        //            data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        //        }
        //    }
        //    return data.ToString().TrimEnd('&');
        //}

        //private static string HmacSha512(string key, string data)
        //{
        //    using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        //    byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        //    return string.Concat(hashValue.Select(b => b.ToString("x2")));
        //}

        //public string GetIpAddress(HttpContext context)
        //{
        //    try
        //    {
        //        var ip = context.Connection.RemoteIpAddress;
        //        return ip?.ToString() ?? "127.0.0.1";
        //    }
        //    catch
        //    {
        //        return "127.0.0.1";
        //    }
        //}
        //public PaymentResponseModel GetFullResponseData(IQueryCollection collection, string hashSecret)
        //{
        //    var responseModel = new PaymentResponseModel();
        //    foreach (var (key, value) in collection)
        //    {
        //        if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
        //        {
        //            AddResponseData(key, value);
        //        }
        //    }

        //    var orderId = GetResponseData("vnp_TxnRef");
        //    var transactionId = GetResponseData("vnp_TransactionNo");
        //    var responseCode = GetResponseData("vnp_ResponseCode");
        //    var secureHash = collection["vnp_SecureHash"];

        //    // Validate the secure hash
        //    var isValidSignature = ValidateSignature(secureHash, hashSecret);
        //    if (!isValidSignature)
        //    {
        //        responseModel.Success = false;
        //        responseModel.VnPayResponseCode = "Invalid Signature";
        //        return responseModel;
        //    }

        //    // Map data to response model
        //    responseModel.Success = responseCode == "00";
        //    responseModel.OrderId = orderId;
        //    responseModel.TransactionId = transactionId;
        //    responseModel.VnPayResponseCode = responseCode;
        //    responseModel.OrderDescription = GetResponseData("vnp_OrderInfo");
        //    responseModel.PaymentMethod = "VnPay";

        //    return responseModel;
        //}
    }
}
