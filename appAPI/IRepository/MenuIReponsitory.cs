using appAPI.Models;

namespace AppAPI.IRepository
{
    public interface MenuIReponsitory
    {
        Task<List<Menus>> GetAllMenus();
        Task<Menus> GetByIdMenus(long id);
        Task CreateMenu(Menus menu);
        Task UpdateMenu(Menus menu);
        Task DeleteMenu(long id);
        Task<List<Menu_items>> GetAllMenuItems();
        Task<Menu_items> GetByIdMenuItems(long id);
        Task CreateMenuItems(Menu_items menu_Items);
        Task UpdateMenuItems(Menu_items menu_items);
        Task DeleteMenuItems(long id);
    }
}
