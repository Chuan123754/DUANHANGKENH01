﻿using Newtonsoft.Json;
using System.Text;
using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class AttributesDiscountServices : IAttributesDiscountServices
    {
        private readonly HttpClient _httpClient;
        public AttributesDiscountServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<P_attribute_discount>> GetAll()
        {
            string requestURL = "https://localhost:7011/api/VariantsDiscount";
            var request = await _httpClient.GetStringAsync(requestURL);
            var item = JsonConvert.DeserializeObject<List<P_attribute_discount>>(request);
            return item;
        }

        public async Task<List<Product_Attributes>> GetProductvariants()
        {
            string requestURL = "https://localhost:7011/api/VariantsDiscount/GetProductvariants";
            var request = await _httpClient.GetStringAsync(requestURL);
            var item = JsonConvert.DeserializeObject<List<Product_Attributes>>(request);
            return item;
        }

        public async Task<List<Discount>> GetDiscount()
        {
            string requestURL = "https://localhost:7011/api/VariantsDiscount/GetDiscount";
            var request = await _httpClient.GetStringAsync(requestURL);
            var item = JsonConvert.DeserializeObject<List<Discount>>(request);
            return item;
        }
        public async Task<P_attribute_discount> Details(long id)
        {
            string requestURL = $"https://localhost:7011/api/VariantsDiscount/{id}";
            var request = await _httpClient.GetStringAsync(requestURL);
            var item = JsonConvert.DeserializeObject<P_attribute_discount>(request);
            return item;
        }
        public async Task<P_attribute_discount> Create(P_attribute_discount vd)
        {
            string requestURL = $"https://localhost:7011/api/VariantsDiscount";
            var jsonContent = JsonConvert.SerializeObject(vd);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = await _httpClient.PostAsync(requestURL, content);
            if (!request.IsSuccessStatusCode)
            {
                var error = await request.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi tạo mới variants discount: {request.StatusCode}, {request.ReasonPhrase}, {error}");
            }

            var requestContent = await request.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<P_attribute_discount>(requestContent);

            return item;
        }
              
        public async Task<bool> Update(P_attribute_discount vd)
        {
            string requestURL = "https://localhost:7011/api/VariantsDiscount";
            var jsonContent = JsonConvert.SerializeObject(vd);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = await _httpClient.PutAsync(requestURL, content);
            if (!request.IsSuccessStatusCode)
            {
                var error = await request.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi khi sửa đối tượng variants discount: {request.StatusCode}, {request.ReasonPhrase}, {error}");
            }

            return request.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(long id)
        {
            string requestURL = $"https://localhost:7011/api/VariantsDiscount/{id}";
            var request = await _httpClient.DeleteAsync(requestURL);

            if (!request.IsSuccessStatusCode)
            {
                var error = await request.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi khi sửa đối tượng variants discount: {request.StatusCode}, {request.ReasonPhrase}, {error}");
            }

            return request.IsSuccessStatusCode;
        }
    }
}