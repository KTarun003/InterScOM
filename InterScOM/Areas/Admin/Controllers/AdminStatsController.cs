using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InterScOM.Data;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Admin.Models;

namespace InterScOM.Areas.Admin.Controllers
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
            var Orders = await _context.VendorOrders.ToListAsync();
            var Vendors = await _context.Vendor.ToListAsync();
            var Supplies = await _context.Supplies.ToListAsync();
            var Fees = await _context.Fee.ToListAsync();

            AdminStats admstats = new AdminStats
            {
                Totalorders = Orders.Count(),
                TotalVendors = Vendors.Count(),
                TotalFees = Fees.Count(),
                Totalsupplies = Supplies.Count()
            };
            foreach(var order in Orders)
            {
                if (order.Status.Equals("Placed"))
                {
                    admstats.OrdersPlaced++;
                }
                else if(order.Status.Equals("Received"))
                {
                    admstats.OrdersReceived++;
                }
                else if(order.Status.Equals("Shipped") && admstats.OrdersShipping.Count <= 5)
                {
                    admstats.OrdersShipping.Add(order);
                }
            }
            
            
            
            return View(admstats);
        }
    }
}
