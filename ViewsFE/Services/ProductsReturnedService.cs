﻿using ViewsFE.IServices;
using ViewsFE.Models;

namespace ViewsFE.Services
{
    public class ProductsReturnedService : IProductsReturnedService
    {
        private readonly HttpClient _httpClient;

        public ProductsReturnedService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<Products_Returned>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7011/api/ProductsReturned");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Products_Returned>>() ?? new List<Products_Returned>();
            }
            else
            {
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<Products_Returned?> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/ProductsReturned/{id}");

            return await response.Content.ReadFromJsonAsync<Products_Returned>();
        }

        public async Task<List<Products_Returned>> GetByOrderDetailIdAsync(long orderDetailId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/ProductsReturned/GetByOrderDetailId?orderDetailId={orderDetailId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Products_Returned>>() ?? new List<Products_Returned>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<Products_Returned> CreateAsync(Products_Returned productReturned)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7011/api/ProductsReturned", productReturned);

            if (response.IsSuccessStatusCode)
            {
                var createdProductReturned = await response.Content.ReadFromJsonAsync<Products_Returned>();
                if (createdProductReturned == null)
                {
                    throw new Exception("Không thể đọc dữ liệu sản phẩm vừa được tạo.");
                }
                return createdProductReturned;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> ProcessReturnAsync(long id)
        {
            var response = await _httpClient.PutAsync($"https://localhost:7011/api/ProductsReturned/ProcessReturn/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> ProcessReturnQuantityAsync(long orderDetailId, int returnQuantity)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7011/api/ProductsReturned/ProcessReturnQuantity",
                new { OrderDetailId = orderDetailId, ReturnQuantity = returnQuantity }
            );

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<Products_Returned?> UpdateAsync(Products_Returned productReturned)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7011/api/ProductsReturned/put", productReturned);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Products_Returned>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {errorContent}");
            }
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7011/api/ProductsReturned/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
