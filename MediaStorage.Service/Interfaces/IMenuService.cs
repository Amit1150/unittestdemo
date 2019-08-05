using System.Collections.Generic;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Menu;

namespace MediaStorage.Service
{
    public interface IMenuService
    {
        ServiceResult AddMenu(MenuViewModel entity);
        List<MenuViewModel> GetAllMenus();
        List<CustomSelectListItem> GetAllMenusBySelectListItem(int? id);
        MenuViewModel GetMenuById(int id);
        ServiceResult RemoveMenu(int id, bool cascadeRemove = false);
        ServiceResult UpdateMenu(MenuViewModel entity);
    }
}