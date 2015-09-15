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

namespace shippingCodefirstTest.Controllers {

    public class UserLabelsController:Controller {
        private UserProfileDataModel db = new UserProfileDataModel();

        // GET: UserLabels
        public async Task<ActionResult> Index() {
            var temp = User.Identity.GetUserId();
            var labels = db.Labels.Include(l => l.OrderHistory).Include(l => l.User).Where(l => l.user_id==temp);
            return View(await labels.ToListAsync());
        }

        // GET: UserLabels/Details/5
        public async Task<ActionResult> Details(int? id) {
            if(id==null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if(label==null) {
                return HttpNotFound();
            }
            return View(label);
        }

        // GET: UserLabels/Create
        public ActionResult Create() {
            ViewBag.order_id=new SelectList(db.OrderHistories,"OrderId","note");
            ViewBag.user_id=new SelectList(db.AspNetUsers,"Id","Email");
            return View();
        }

        // POST: UserLabels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection f) {
            //public async Task<ActionResult> Create([Bind(Include = "LabelId,ver,lb_content,state,created_time,last_updated_time,order_id,user_id")] Label label){
            Label label = new Label();
            label_content_xml lb_c = new label_content_xml();
            if(ModelState.IsValid) {
                OrderHistory oh = new OrderHistory() {
                    //add properties
                    total_cost_price=0,
                    total_sell_balance=0,
                    total_sell_tax=0,
                    total_sell_price=0,
                    state=(int)OrderState.Created,
                    payment_id="123",
                    user_id=User.Identity.GetUserId()
                };
                lb_c.l=label;
                lb_c.from_info.Add("from_name",Request.Form["from_name"]);
                lb_c.from_info.Add("from_address_1",Request.Form["from_address_1"]);
                lb_c.from_info.Add("from_address_2",Request.Form["from_address_2"]);
                lb_c.from_info.Add("from_zipcode",Request.Form["from_zipcode"]);
                lb_c.from_info.Add("from_email",Request.Form["from_email"]);
                lb_c.from_info.Add("from_telephone",Request.Form["from_telephone"]);
                lb_c.from_info.Add("from_nation",Request.Form["from_nation"]);
                lb_c.from_info.Add("from_city",Request.Form["from_city"]);
                lb_c.from_info.Add("from_nation_state",Request.Form["from_nation_state"]);

                lb_c.from_info.Add("to_name",Request.Form["to_name"]);
                lb_c.from_info.Add("to_address_1",Request.Form["to_address_1"]);
                lb_c.from_info.Add("to_address_2",Request.Form["to_address_2"]);
                lb_c.from_info.Add("to_zipcode",Request.Form["to_zipcode"]);
                lb_c.from_info.Add("to_email",Request.Form["to_email"]);
                lb_c.from_info.Add("to_telephone",Request.Form["to_telephone"]);
                lb_c.from_info.Add("to_nation",Request.Form["to_nation"]);
                lb_c.from_info.Add("to_city",Request.Form["to_city"]);
                lb_c.from_info.Add("to_nation_state",Request.Form["to_nation_state"]);



                label.lb_content=lb_c.generateXml().ToString();
                label.ver="s_1";
                label.state=(int)LabelState.Created;
                label.order_id=db.OrderHistories.Add(oh).OrderId;
                label.user_id=User.Identity.GetUserId();
                db.Labels.Add(label);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.order_id=new SelectList(db.OrderHistories,"OrderId","note",label.order_id);
            ViewBag.user_id=new SelectList(db.AspNetUsers,"Id","Email",label.user_id);
            return View(label);
        }

        // GET: UserLabels/Edit/5
        public async Task<ActionResult> Edit(int? id) {
            if(id==null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if(label==null) {
                return HttpNotFound();
            }
            ViewBag.order_id=new SelectList(db.OrderHistories,"OrderId","note",label.order_id);
            ViewBag.user_id=new SelectList(db.AspNetUsers,"Id","Email",label.user_id);
            return View(label);
        }

        // POST: UserLabels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LabelId,ver,lb_content,state,created_time,last_updated_time,order_id,user_id")] Label label) {
            if(ModelState.IsValid) {
                db.Entry(label).State=EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.order_id=new SelectList(db.OrderHistories,"OrderId","note",label.order_id);
            ViewBag.user_id=new SelectList(db.AspNetUsers,"Id","Email",label.user_id);
            return View(label);
        }

        // GET: UserLabels/Delete/5
        public async Task<ActionResult> Delete(int? id) {
            if(id==null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Label label = await db.Labels.FindAsync(id);
            if(label==null) {
                return HttpNotFound();
            }
            return View(label);
        }

        // POST: UserLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Label label = await db.Labels.FindAsync(id);
            db.Labels.Remove(label);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
