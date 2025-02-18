﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewsFE.Models;
using ViewsFE.IServices;
using System.Collections.Generic;

namespace ViewsFE.Services
{
    public class UserVoucherService : IUserVoucherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserVoucherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        // Lấy danh sách tất cả các UserVouchers từ API
        public async Task<List<UserVouchers>> GetAll()
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers";
            var response = await _httpClient.GetStringAsync(requestURL);
            var userVouchers = JsonConvert.DeserializeObject<List<UserVouchers>>(response);
            return userVouchers;
        }

        // Lấy chi tiết UserVoucher theo ID từ API
        public async Task<UserVouchers> Details(long id)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            var userVoucher = JsonConvert.DeserializeObject<UserVouchers>(response);
            return userVoucher;
        }

        // Lấy danh sách UserVoucher theo VoucherId từ API
        public async Task<List<UserVouchers>> GetByVoucherId(long voucherId)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/byVoucher/{voucherId}";
            var response = await _httpClient.GetStringAsync(requestURL);
            var userVouchers = JsonConvert.DeserializeObject<List<UserVouchers>>(response);
            return userVouchers;
        }

        public async Task<UserVouchers> GetByVoucherIdAndUserId(long voucherId, long userId)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/byVoucherAndUser/{voucherId}/{userId}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<UserVouchers>(response);
        }


        // Tạo mới một UserVoucher
        public async Task<bool> Create(UserVouchers userVoucher)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/post";
            var jsonContent = JsonConvert.SerializeObject(userVoucher);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create UserVoucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }


        // Cập nhật một UserVoucher
        public async Task<bool> Update(UserVouchers userVoucher)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/put";
            var jsonContent = JsonConvert.SerializeObject(userVoucher);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to update UserVoucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }

        // Xóa một UserVoucher theo ID
        public async Task<bool> Delete(long id)
        {
            string requestURL = $"{_baseUrl}/api/UserVouchers/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete UserVoucher. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }
    }
}
