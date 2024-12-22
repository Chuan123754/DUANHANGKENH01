//using appAPI.IRepository;
//using appAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace appAPI.Repository
//{
//    public class MenuReponsitory : MenuIReponsitory
//    {
//        private readonly APP_DATA_DATN _context;
//        public MenuReponsitory(APP_DATA_DATN context)
//        {
//            _context = context;
//        }
//        public async Task CreateMenu(Menus menu)
//        {
//           _context.Menus.Add(menu);
//            await _context.SaveChangesAsync();
//        }

//        public async Task CreateMenuItems(Menu_items menu_Items)
//        {

//            _context.Menu_items.Add(menu_Items);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteMenu(long id)
//        {
//            var menu = await GetByIdMenus(id);
//            _context.Menus.Remove(menu);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteMenuItems(long id)
//        {
//            var menuItem = await GetByIdMenuItems(id);
//            _context.Menu_items.Remove(menuItem);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<Menu_items>> GetAllMenuItems()
//        {
//           return await _context.Menu_items.ToListAsync();
//        }

//        public async Task<List<Menus>> GetAllMenus()
//        {
//           return await _context.Menus.ToListAsync();
//        }

//        public async Task<Menu_items> GetByIdMenuItems(long id)
//        {
//            return await _context.Menu_items.FindAsync(id);
//        }

//        public async Task<Menus> GetByIdMenus(long id)
//        {
//            return await _context.Menus.FindAsync(id);
//        }

//        public async Task UpdateMenu(Menus menu)
//        {
//           _context.Entry(menu).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateMenuItems(Menu_items menu_items)
//        {
//          _context.Entry(menu_items).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }
//    }
//}
