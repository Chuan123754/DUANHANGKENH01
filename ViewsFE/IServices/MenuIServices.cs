using ViewsFE.Models;
namespace ViewsFE.IServices
{
    public interface MenuIServices
    {
        Task<List<Menus>> GetAllMenus();
        Task<Menus> GetByIdMenus(long id);
        Task CreateMenu(Menus menu);
        Task UpdateMenu(Menus menu);
        Task DeleteMenu(long id);
        Task<List<Menu_items>> GetAllMenuItems();
        Task<Menus> GetByIdMenuItems(long id);
        Task CreateMenuItems(Menu_items menu_Items);
        Task UpdateMenuItems(Menu_items menu_items);
        Task DeleteMenuItems(long id);
    }
}
