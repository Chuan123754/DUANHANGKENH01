﻿using ViewsFE.Models;
using static ViewsFE.Services.ProductsReturnedService;

namespace ViewsFE.IServices
{
    public interface IProductsReturnedService
    {
        Task<List<Products_Returned>> GetAllAsync();
        Task<Products_Returned?> GetByIdAsync(long id);
        Task<List<Products_Returned>> GetByOrderDetailIdAsync(long orderDetailId);
        Task<List<Products_Returned>> GetAllWithDetailsAsync();
        Task<Products_Returned> CreateAsync(Products_Returned productReturned);
        Task<bool> ProcessReturnAsync(long id);
        Task<bool> ProcessbackAsync(long id);
        Task<bool> ProcessReturnQuantityAsync(long orderDetailId, int returnQuantity, long roductReturnedId);
        Task<bool> ProcessRefundQuantityAsync(long orderDetailId, int returnQuantity, long roductReturnedId);
        Task<Products_Returned?> UpdateAsync(Products_Returned productReturned);
        Task<bool> UpdateReturnQuantityAsync(long id, int quantityToStock);
        Task<bool> DeleteAsync(long id);
    }
}
