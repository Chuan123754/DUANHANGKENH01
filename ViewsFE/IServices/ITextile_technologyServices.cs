﻿using ViewsFE.Models;

namespace ViewsFE.IServices
{
    public interface ITextile_technologyServices
    {
        Task<List<Textile_technology>> GetAll();
        Task<Textile_technology> Details(long id);
        Task Create(Textile_technology t);
        Task Update(Textile_technology t);
        Task Delete(long id);
        Task<List<Textile_technology>> Search(string keyword);
    }
}