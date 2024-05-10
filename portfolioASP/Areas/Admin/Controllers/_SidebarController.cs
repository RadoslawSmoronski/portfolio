using Microsoft.AspNetCore.Mvc;

namespace portfolioASP.Areas.Admin.Controllers
{
    public class _SidebarController : Controller
    {
        public IActionResult _Sidebar()
        {
            return PartialView();
        }

    }
}
