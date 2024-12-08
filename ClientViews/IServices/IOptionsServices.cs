﻿using ClientViews.Models;

namespace ClientViews.IServices
{
    public interface IOptionsServices
    {
        Task<List<Options>> GetAll();
        Task<Options> Details(long id);
        Task Create(Options op);
        Task Update(Options op);
        Task Delete(long id);
    }
}
