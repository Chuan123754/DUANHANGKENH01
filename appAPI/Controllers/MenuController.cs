using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppAPI.IRepository;
using AppAPI.Repository;
using appAPI.Models;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuReponsitory _repon;
        public MenuController()
        {
            _repon = new MenuReponsitory();
        }

        [HttpGet("GetAllMenus")]
        public async Task<List<Menus>> GetAllMenus()
        {
            return await _repon.GetAllMenus();
        }
        [HttpGet("GetAllMenuItems")]
        public async Task<List<Menu_items>> GetAllMenuItems()
        {
            return await _repon.GetAllMenuItems();
        }

        [HttpGet("MenuDetails")]
        public async Task<Menus> DetailsMenu(long id)
        {
            return await _repon.GetByIdMenus(id);
        }

        [HttpGet("MenuItemsDetails")]
        public async Task<Menu_items> DetailsMenuItems(long id)
        {
            return await _repon.GetByIdMenuItems(id);
        }

        [HttpPost("CreateMenus")]
        public async Task CreateMenus(Menus menus)
        {
            await _repon.CreateMenu(menus);
        }
        [HttpPost("CreateMenuItems")]
        public async Task CreateMenuItems(Menu_items menu_Items)
        {
            await _repon.CreateMenuItems(menu_Items);
        }

        [HttpPut("UpdateMenus")]
        public async Task UpdateMenus(Menus menus)
        {
            await _repon.UpdateMenu(menus);
        }
        [HttpPut("UpdateMenuItems")]
        public async Task UpdateMenuItems(Menu_items menu_Items)
        {
            await _repon.UpdateMenuItems(menu_Items);
        }

        [HttpDelete("DeleteMenus")]
        public async Task DeleteMenus(long id)
        {
            await _repon.DeleteMenu(id);
        }
        [HttpDelete("DeleteMenuItems")]
        public async Task DeleteMenuItems(long id)
        {
            await _repon.DeleteMenuItems(id);
        }
    }
}
