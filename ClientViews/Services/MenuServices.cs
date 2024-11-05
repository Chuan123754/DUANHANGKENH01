using ClientViews.IServices;
using ClientViews.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
namespace ClientViews.Services
{
    public class MenuServices : MenuIServices
    {
        HttpClient _clitent;
        public MenuServices()
        {
            _clitent = new HttpClient();

           
        }
        public async Task CreateMenu(Menus menu)
        {
            await _clitent.PostAsJsonAsync("api/Menu/CreateMenus", menu);
        }

        public Task CreateMenuItems(Menu_items menu_Items)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMenu(long id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMenuItems(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Menu_items>> GetAllMenuItems()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Menus>> GetAllMenus()
        {
            string requestURL = $@"https://localhost:7015/api/Menu/GetAllMenus";
            var response = await _clitent.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Menus>>(response);
        }

        public Task<Menus> GetByIdMenuItems(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Menus> GetByIdMenus(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMenu(Menus menu)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMenuItems(Menu_items menu_items)
        {
            throw new NotImplementedException();
        }
    }
}
