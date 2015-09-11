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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace shippingCodefirstTest.Controllers
{
    public class UserLabelsController : Controller
    {
        private UserProfileDataModel db = new UserProfileDataModel();

        // GET: UserLabels
        public async Task<ActionResult> Index()
        {
            var labels = db.Labels.Include(l => l.OrderHistory).Include(l => l.User).Where(l=>l.user_id==User.Identity.GetUserId());
            return View(await labels.ToListAsync());
        }

        // GET: UserLabels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return HttpNotFound();
            }
            return View(label);
        }

        // GET: UserLabels/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.OrderHistories, "OrderId", "note");
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: UserLabels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LabelId,ver,lb_content,state,created_time,last_updated_time,order_id,user_id")] Label label)
        {
            if (ModelState.IsValid)
            {
                db.Labels.Add(label);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.OrderHistories, "OrderId", "note", label.order_id);
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", label.user_id);
            return View(label);
        }

        // GET: UserLabels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.OrderHistories, "OrderId", "note", label.order_id);
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", label.user_id);
            return View(label);
        }

        // POST: UserLabels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LabelId,ver,lb_content,state,created_time,last_updated_time,order_id,user_id")] Label label)
        {
            if (ModelState.IsValid)
            {
                db.Entry(label).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.OrderHistories, "OrderId", "note", label.order_id);
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", label.user_id);
            return View(label);
        }

        // GET: UserLabels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return HttpNotFound();
            }
            return View(label);
        }

        // POST: UserLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Label label = await db.Labels.FindAsync(id);
            db.Labels.Remove(label);
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
