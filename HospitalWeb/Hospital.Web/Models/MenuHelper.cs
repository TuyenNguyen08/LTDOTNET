using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;

namespace Hospital.Web.Models
{
    public class MenuHelper
    {
        public static async Task<List<MenuViewModel>> GetListMenuAsync(InitParam initParam, int nn = 1)
        {
            var menuViewModel = new List<MenuViewModel>();

            // Lay het tu DB
            var listMenu = await initParam.Db.Menu
                .Where(h => h.FkNgonNgu == nn)
                .OrderBy(h => h.Prioty).ToListAsync();

            // Group cac menu thanh tung nhom theo ID parent
            var groupMenu = listMenu.GroupBy(h => h.Idparrent).ToList();

            // Lay cac menu cha (Idparrent = 0)
            var groupMenuCha = groupMenu.FirstOrDefault(h => h.Key == 0);

            // Lap tren cac menu cha de lay cac menu con
            foreach (var menuCha in groupMenuCha)
            {
                // Tao view model cha
                var menuChaViewModel = new MenuViewModel
                {
                    Menu = menuCha,
                };
                menuViewModel.Add(menuChaViewModel);

                // Lay cac menu con
                var groupMenuCon = groupMenu.FirstOrDefault(h => h.Key == menuCha.Id);
                if (groupMenuCon == null)
                {
                    continue;
                }
                // Tao cac menu con view model
                foreach (var menuCon in groupMenuCon)
                {
                    menuChaViewModel.ListMenu.Add(new MenuViewModel
                    {
                        Menu = menuCon
                    });
                }
            }




            return menuViewModel;

        }
    }
}
