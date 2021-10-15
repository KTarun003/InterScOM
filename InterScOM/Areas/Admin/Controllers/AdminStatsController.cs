using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Admin.Models;
using Web.Data;

namespace Web.Areas.Admin.Controllers
{
    // [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AdminStatsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminStatsController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.List<VendorOrders> Orders = await _context.VendorOrders.ToListAsync();
            System.Collections.Generic.List<Vendor> Vendors = await _context.Vendor.ToListAsync();
            System.Collections.Generic.List<Supplies> Supplies = await _context.Supplies.ToListAsync();
            System.Collections.Generic.List<Fee> Fees = await _context.Fee.ToListAsync();

            AdminStats admstats = new AdminStats
            {
                Totalorders = Orders.Count(),
                TotalVendors = Vendors.Count(),
                TotalFees = Fees.Count(),
                Totalsupplies = Supplies.Count()
            };
            foreach (VendorOrders order in Orders)
            {
                if (order.Status.Equals("Placed"))
                {
                    admstats.OrdersPlaced++;
                }
                else if (order.Status.Equals("Received"))
                {
                    admstats.OrdersReceived++;
                }
                else if (order.Status.Equals("Shipped") && admstats.OrdersShipping.Count <= 5)
                {
                    admstats.OrdersShipping.Add(order);
                }
            }



            return View(admstats);
        }
    }
}
