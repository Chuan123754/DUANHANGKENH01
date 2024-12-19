using appAPI.DTO;
using appAPI.IRepository;
using appAPI.Models;
using appAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace appAPI.Repository
{
    public class ProductAttributesRepository : IProductAttributesRepository
    {
        private readonly APP_DATA_DATN _context;
        private readonly IMemoryCache _cache;

        public ProductAttributesRepository(APP_DATA_DATN context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task Create(Product_Attributes productAttribute)
        {
            await _context.Product_Attributes.AddAsync(productAttribute);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var deleteItem = await _context.Product_Attributes.FindAsync(id);
            _context.Product_Attributes.Remove(deleteItem);
            await _context.SaveChangesAsync();


        }

        public async Task<List<Product_Attributes>> GetAllProductAttributes()
        {
            try
            {
                return await _context.Product_Attributes
                                     .Include(p => p.Size)
                                     .Include(p => p.Color)
                                     .Include(p=>p.Product_Variant)
                                     .Include(p => p.Product_Variant).ThenInclude(p => p.Style)
                                     .Include(p => p.Product_Variant).ThenInclude(p => p.Material)
                                     .Include(p => p.Product_Variant).ThenInclude(p => p.Textile_Technology)
                                     .Include(p => p.Product_Variant).ThenInclude(p => p.Posts)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi, ví dụ:
                Console.WriteLine("Error in GetAllProductAttributes: " + ex.Message);
                return new List<Product_Attributes>();
            }
        }


        public async Task<Product_Attributes> GetProductAttributesById(long id)
        {
            // Kiểm tra xem dữ liệu đã có trong cache chưa
            if (!_cache.TryGetValue(id, out Product_Attributes productAttribute))
            {
                // Nếu không có, thực hiện truy vấn
                productAttribute = await _context.Product_Attributes
                    .Where(p => p.Id == id)
                    .Include(p => p.Color)
                    .Include(p => p.Size)
                    .Include(p => p.Product_Variant)
                    .Include(p => p.Product_Variant).ThenInclude(p => p.Style)
                    .Include(p => p.Product_Variant).ThenInclude(p => p.Material)
                    .Include(p => p.Product_Variant).ThenInclude(p => p.Textile_Technology)
                    .Include(p => p.Product_Variant).ThenInclude(p => p.Posts)
                    .FirstOrDefaultAsync();

                // Nếu tìm thấy, lưu vào cache
                if (productAttribute != null)
                {
                    _cache.Set(id, productAttribute, TimeSpan.FromMinutes(30)); // Thời gian lưu cache
                }
            }

            return productAttribute;
        }

        public async Task<List<Product_Attributes>> GetProductAttributesByProductVarianId(long id)
        {
            return await _context.Product_Attributes
                                .Where(p => p.Product_Variant_Id == id)
                                .Include(p => p.Color)
                                .Include(p => p.Size)
                                .Include(p => p.Product_Variant)
                                .Include(p => p.Product_Variant).ThenInclude(p => p.Style)
                                .Include(p => p.Product_Variant).ThenInclude(p => p.Material)
                                .Include(p => p.Product_Variant).ThenInclude(p => p.Textile_Technology)
                                .Include(p => p.Product_Variant).ThenInclude(p => p.Posts)
                                .ToListAsync();
        }
        public async Task<List<Product_Attributes>> GetProductAttributesByProductVarianIdClient(long id)
        {
            return await _context.Product_Attributes
                              .Where(p => p.Product_Variant_Id == id)
                              .Include(p => p.Color)
                              .Include(p => p.Size)
                              .Include(p => p.Product_Variant).ThenInclude(pt => pt.Posts).ThenInclude(pc=> pc.Post_categories).ThenInclude(pd => pd.Categories)
                              .Include(p => p.Product_Variant).ThenInclude(pt => pt.Posts).ThenInclude(pc=> pc.Post_tags).ThenInclude(pd => pd.Tag)
                              .Include(p => p.Product_Variant).ThenInclude(p => p.Style)
                              .Include(p => p.Product_Variant).ThenInclude(p => p.Material)
                              .Include(p => p.Product_Variant).ThenInclude(p => p.Textile_Technology)
                              .Include(p => p.Product_Variant).ThenInclude(p => p.Posts)
                              .ToListAsync();
        }

        public async Task<List<Product_Attributes_DTO>> GetVariantByProductVariantId(List<long> variantIds)
        {
            if (variantIds == null || !variantIds.Any())
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

        public async Task Update(Product_Attributes productAttribute, long id)
        {
            var updateItem = await _context.Product_Attributes.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.SKU = productAttribute.SKU;
                updateItem.Image = productAttribute.Image;
                updateItem.Regular_price = productAttribute.Regular_price;
                updateItem.Sale_price = productAttribute.Sale_price;
                updateItem.Stock = productAttribute.Stock;
                updateItem.Status = productAttribute.Status;
                updateItem.Color_Id = productAttribute.Color_Id;
                updateItem.Size_Id = productAttribute.Size_Id;

                _context.Product_Attributes.Update(updateItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<ProductDTO>> GetProductDTOsAsync()
        {
            var productDTOs = await _context.Product_Attributes
             .Join(_context.product_variants,
                 pa => pa.Product_Variant_Id,
                 pv => pv.Id,
                 (pa, pv) => new { pa, pv })
             .Join(_context.Posts,
                 combined => combined.pv.Post_Id,
                 pp => pp.Id,
                 (combined, pp) => new { combined.pa, combined.pv, pp })
             .Join(_context.Textile_Technologies,
                 combined => combined.pv.Textile_technology_id,
                 tt => tt.Id,
                 (combined, tt) => new { combined.pa, combined.pv, combined.pp, tt })
             .Join(_context.Styles,
                 combined => combined.pv.Style_id,
                 style => style.Id,
                 (combined, style) => new { combined.pa, combined.pv, combined.pp, combined.tt, style })
             .Join(_context.Materials,
                 combined => combined.pv.Material_id,
                 material => material.Id,
                 (combined, material) => new { combined.pa, combined.pv, combined.pp, combined.tt, combined.style, material })
             .Join(_context.Sizes,
                 combined => combined.pa.Size_Id,
                 size => size.Id,
                 (combined, size) => new { combined.pa, combined.pv, combined.pp, combined.tt, combined.style, combined.material, size })
             .Join(_context.Color,
                 combined => combined.pa.Color_Id,
                 color => color.Id,
                 (combined, color) => new ProductDTO
                 {
                     ProductName = combined.pp.Title,            // Tên sản phẩm (Title của Product_Post)
                     Image = combined.pa.Image,                  // Hình ảnh (Image của Product_Attributes)
                     Status = combined.pa.Status,                // Trạng thái (Status của Product_Attributes)
                     Description = combined.pv.Description,      // Mô tả (Description của Product_Variants)
                     TextileTechnology = combined.pv.Textile_Technology.Title,     // Công nghệ dệt (Title của Textile_Technologies)
                     Style = combined.pv.Style.Title,             // Phong cách (Title của Styles)
                     Material = combined.pv.Material.Title,      // Chất liệu (Title của Materials)
                     Size = combined.pa.Size.Title,               // Size (Title của Sizes)
                     Color = combined.pa.Color.Title,                          // Màu (Title của Color)
                     Stock = combined.pa.Stock,                  // Số lượng (Stock của Product_Attributes)
                     RegularPrice = combined.pa.Regular_price   // Giá tiền (Regular_price của Product_Attributes)
                 })
             .ToListAsync();

            return productDTOs;
        }

        public async Task<List<Product_Attributes>> GetByTypeAsync(int pageNumber, int pageSize, string searchTerm)
        {
            // Giả sử `Product_Attributes` có `ProductPostId` để liên kết với `Product_Post`.
            return await _context.Product_Attributes
                .Where(p => p.Product_Variant.Posts.Deleted == false &&
                       (string.IsNullOrEmpty(searchTerm) || p.Product_Variant.Material.Title.Contains(searchTerm)
                       || p.Product_Variant.Textile_Technology.Title.Contains(searchTerm) ||
                       _context.Posts.Any(post => post.Id == p.Product_Variant.Post_Id && post.Title.Contains(searchTerm)
                       || p.Size.Title.Contains(searchTerm) || p.Color.Title.Contains(searchTerm))))
                .OrderBy(p => p.Product_Variant.Posts.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> GetTotalCountAsync(string searchTerm)
        {
            return await _context.Product_Attributes
               .CountAsync(p => p.Product_Variant.Posts.Deleted == false &&
                             (string.IsNullOrEmpty(searchTerm) || p.Product_Variant.Material.Title.Contains(searchTerm)
                       || p.Product_Variant.Textile_Technology.Title.Contains(searchTerm) ||
                       _context.Posts.Any(post => post.Id == p.Product_Variant.Post_Id && post.Title.Contains(searchTerm)
                       || p.Size.Title.Contains(searchTerm) || p.Color.Title.Contains(searchTerm))));
        }
    }
}
