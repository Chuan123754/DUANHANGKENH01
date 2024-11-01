﻿using appAPI.Models;

namespace appAPI.IRepository
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAll();
        Task<Categories> Details(long id);
        Task Create(Categories c);
        Task Update(Categories c);
        Task Delete(long id);
        Task<List<Categories>> GetByTypeAsync(string type,int pageNumber, int pageSize, string searchTerm);
        Task<int> GetTotalCountAsync(string type, string searchTerm);
    }
}
