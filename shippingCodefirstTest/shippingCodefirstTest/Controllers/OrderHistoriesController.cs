using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shippingCodefirstTest.Models;

namespace shippingCodefirstTest.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderHistoriesController : Controller
    {
        private UserProfileDataModel db = new UserProfileDataModel();


        // GET: OrderHistories
        public async Task<ActionResult> Index()
        {
            var orderHistories = db.OrderHistories.Include(o => o.User);
            return View(await orderHistories.ToListAsync());
        }

        // GET: OrderHistories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHistory orderHistory = await db.OrderHistories.FindAsync(id);
            if (orderHistory == null)
            {
                return HttpNotFound();
            }
            return View(orderHistory);
        }

        // GET: OrderHistories/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: OrderHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,total_sell_price,total_cost_price,total_sell_tax,total_sell_balance,note,state,payment_id,created_time,last_updated_time,paid_on_time,user_id")] OrderHistory orderHistory)
        {
            if (ModelState.IsValid)
            {
                db.OrderHistories.Add(orderHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", orderHistory.user_id);
            return View(orderHistory);
        }

        // GET: OrderHistories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHistory orderHistory = await db.OrderHistories.FindAsync(id);
            if (orderHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", orderHistory.user_id);
            return View(orderHistory);
        }

        // POST: OrderHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,total_sell_price,total_cost_price,total_sell_tax,total_sell_balance,note,state,payment_id,created_time,last_updated_time,paid_on_time,user_id")] OrderHistory orderHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", orderHistory.user_id);
            return View(orderHistory);
        }

        // GET: OrderHistories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderHistory orderHistory = await db.OrderHistories.FindAsync(id);
            if (orderHistory == null)
            {
                return HttpNotFound();
            }
            return View(orderHistory);
        }

        // POST: OrderHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderHistory orderHistory = await db.OrderHistories.FindAsync(id);
            db.OrderHistories.Remove(orderHistory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
