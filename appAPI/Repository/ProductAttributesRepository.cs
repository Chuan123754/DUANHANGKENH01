﻿using appAPI.DTO;
using appAPI.IRepository;
using appAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appAPI.Repository
{
    public class ProductAttributesRepository : IProductAttributesRepository
    {
        private readonly APP_DATA_DATN _context;

        public ProductAttributesRepository(APP_DATA_DATN context)
        {
            _context = context;
        }
        public async Task Create(Product_Attributes productAttribute)
        {
            await _context.Product_Attributes.AddAsync(productAttribute);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var deleteItem = await _context.Product_Attributes.FindAsync(id);
            if(deleteItem!= null)
            {
                _context.Product_Attributes.Remove(deleteItem);  
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotImplementedException("Not Found");
            }

        }

        public async Task<List<Product_Attributes>> GetAllProductAttributes()
        {
            var lst_ProductAttributes = await _context.Product_Attributes
                                                    .Include(p=>p.Size)
                                                    .Include(p=>p.Color)
                                                    .Include(p=>p.Product_Variant).ThenInclude(p=>p.Style)
                                                    .Include(p=>p.Product_Variant).ThenInclude(p=>p.Material)
                                                    .Include(p=>p.Product_Variant).ThenInclude(p=>p.Textile_Technology)
                                                    .ToListAsync();
            if (lst_ProductAttributes.Count <= 0)
            {
                throw new NotImplementedException("Not Found");
            }
            return lst_ProductAttributes;
        }

        public async Task<Product_Attributes> GetProductAttributesById(long id)
        {
            var productAttribute = await _context.Product_Attributes.Where(p=>p.Id==id)
                                                                    .Include(p=>p.Color)
                                                                    .Include(p=>p.Size)
                                                                    .Include(p=>p.Product_Variant)
                                                                    .FirstOrDefaultAsync();
            if (productAttribute != null)
            {
                return productAttribute;
            }
            else
            {
                throw new NotImplementedException("Not Found");
            }

        }

        public async Task<List<Product_Attributes>> GetProductAttributesByProductVarianId(long id)
        {
            return await _context.Product_Attributes
                                .Where(p=>p.Product_Variant_Id==id)
                                .Include(p=>p.Color)
                                .Include(p=>p.Size)
                                .Include (p=>p.Product_Variant)
                                .ToListAsync();
        }

        public async Task<List<Product_Attributes_DTO>> GetVariantByProductVariantId(List<long> variantIds)
        {
            if(variantIds == null || !variantIds.Any())
            {
                return new List<Product_Attributes_DTO>();
            }

            var productAttributes = await _context.Product_Attributes
                .Where(p => variantIds.Contains(p.Product_Variant_Id))
                .Select(p => new Product_Attributes_DTO
                {
                    Id = p.Id,
                    Name = p.Product_Variant.Posts.Title,
                    Description = p.Product_Variant.Posts.Description,
                    Material_Title = p.Product_Variant.Material.Title,
                    Style_Title = p.Product_Variant.Style.Title,
                    Text_Tile_Technologies_Title = p.Product_Variant.Textile_Technology.Title,
                    Size_Title = p.Size.Title,
                    Color_Title = p.Color.Title
                }).ToListAsync();
            return productAttributes;
        }

        public async Task Update(Product_Attributes productAttribute)
        {
            if (await GetProductAttributesById(productAttribute.Id) == null)
            {
                throw new KeyNotFoundException("Not Found");
            }
            _context.Entry(productAttribute).State = EntityState.Modified;
        }
    }
}