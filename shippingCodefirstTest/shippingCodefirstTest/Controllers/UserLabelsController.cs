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
using EasyPost;
using System.Xml.Linq;
using shippingCodefirstTest.ViewModels;
using Stripe;
using System.Configuration;

namespace shippingCodefirstTest.Controllers {
    [Authorize]
    public class UserLabelsController:Controller {
        private UserProfileDataModel db = new UserProfileDataModel();
        private Label lb;
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
            this.lb=await db.Labels.FindAsync(id);
            Session["temp_lb"]=this.lb;
            if(lb==null) {
                return HttpNotFound();
            }
            return View(lb);
        }

        [HttpPost]
        public async Task<ActionResult> Details(int? id,FormCollection f) {
            var label = (Label)Session["temp_lb"];
            string rateID = Request.Form["rate.rate"];
            label.lookupPrice.Buy(rateID);
            XmlViewModel xvm = XmlHelper.Deserialize<XmlViewModel>(label.lb_content);
            //xvm.shipping_info=new ViewModels.shipping_info();
            xvm.shipping_info.tracking_code=label.lookupPrice.tracking_code; 
            xvm.shipping_info.shipping_label_address=label.lookupPrice.postage_label.label_url;
            Rate chosenRate = new Rate();
            foreach(Rate rate in label.lookupPrice.rates) {
                if(rate.id==rateID) chosenRate=rate;
            }
            lb=await db.Labels.FindAsync(id);
            lb.lb_content=XmlHelper.Serialize(xvm);
            lb.OrderHistory.total_cost_price=Decimal.Parse(chosenRate.rate);
            db.Entry(lb).State=EntityState.Modified;
            //
            //db.Labels.Attach(lb);
            await db.SaveChangesAsync();
            string returnUrl = xvm.shipping_info.shipping_label_address;
            //ViewBag.ReturnUrl=returnUrl;
            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult Summary() {
            var label = (Session["temp_lb"] as Label);
            string rateID = Request.Form["rate.rate"];
            Rate chosenRate = new Rate();

            foreach(Rate rate in label.lookupPrice.rates) {
                if(rate.id==rateID) {
                    label.chosenRate=rate;
                    chosenRate=rate;
                    ViewBag.ChosenRate=chosenRate;
                    ViewBag.final_price_cent=Decimal.Parse(chosenRate.rate)*100;
                    break;
                }
            }
            return View(label);
        }

        public async Task<ActionResult> PaidInfo(FormCollection f) {
            //chosen Rate p: resubmit, old form
            //charge the money from the token
            // charge from the bal//wrong goto payment
            //buy the label
            //if wrong return the money
            //save label info
            //return index
            var label = Session["temp_lb"] as Label;
            //var myToken = new StripeTokenCreateOptions();
            var label_charge = new StripeChargeCreateOptions() {
                Amount=Decimal.ToInt32(Decimal.Parse(label.chosenRate.rate)*100),
                Currency="usd",
                Source=new StripeSourceOptions {
                    //TokenId=
                    TokenId=Request.Form["stripeToken"]
                }
                
            };
            //var requestOptions = new StripeRequestOptions();
            //requestOptions.ApiKey="sk_test_2B7FGWX2f9zv1jTnlL4wtlIC";
            //var planService = new StripePlanService("sk_test_2B7FGWX2f9zv1jTnlL4wtlIC");
            var chargeService = new StripeChargeService();
            chargeService.ApiKey=ConfigurationManager.AppSettings["StripeApiKey"];
            var stripeCharge = chargeService.Create(label_charge);
            if(stripeCharge.Status=="succeeded") {
                label.lookupPrice.Buy(label.chosenRate.id);
                label.lb_obj.shipping_info.tracking_code=label.lookupPrice.tracking_code;
                label.lb_obj.shipping_info.shipping_label_address=label.lookupPrice.postage_label.label_url;

                //lb=await db.Labels.FindAsync(id);
                db.Labels.Attach(label);
                label.lb_content=XmlHelper.Serialize(label.lb_obj);
                label.OrderHistory.total_cost_price=Convert.ToDecimal(stripeCharge.Amount/100.0);
                db.Entry(label).State=EntityState.Modified;
                //
                //db.Labels.Attach(lb);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: UserLabels/Create
        public ActionResult Create() {
            ViewBag.order_id=new SelectList(db.OrderHistories,"OrderId","note");
            ViewBag.user_id=new SelectList(db.AspNetUsers,"Id","Email");
            return View();
        }

        public async Task<ActionResult> PrintLabel(int? id) {
            Label label= await db.Labels.FindAsync(id);
            return Redirect(label.lb_obj.shipping_info.shipping_label_address);
        }
        // POST: UserLabels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(XmlViewModel xvm) {
            //public async Task<ActionResult> Create([Bind(Include = "LabelId,ver,lb_content,state,created_time,last_updated_time,order_id,user_id")] Label label){
            Label label = new Label();
            //label_content_xml lb_c = new label_content_xml();
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
                //xvm.shipping_info=new ViewModels.shipping_info();
                label.lb_content=XmlHelper.Serialize(xvm);
                //label.l_xml.l=label;
                //label.l_xml.from_info.Add("from_name",Request.Form["from_name"]);
                //label.l_xml.from_info.Add("from_address_1",Request.Form["from_address_1"]);
                //label.l_xml.from_info.Add("from_address_2",Request.Form["from_address_2"]);
                //label.l_xml.from_info.Add("from_zipcode",Request.Form["from_zipcode"]);
                //label.l_xml.from_info.Add("from_email",Request.Form["from_email"]);
                //label.l_xml.from_info.Add("from_telephone",Request.Form["from_telephone"]);
                //label.l_xml.from_info.Add("from_nation",Request.Form["from_nation"]);
                //label.l_xml.from_info.Add("from_city",Request.Form["from_city"]);
                //label.l_xml.from_info.Add("from_nation_state",Request.Form["from_nation_state"]);

                //label.l_xml.to_info.Add("to_name",Request.Form["to_name"]);
                //label.l_xml.to_info.Add("to_address_1",Request.Form["to_address_1"]);
                //label.l_xml.to_info.Add("to_address_2",Request.Form["to_address_2"]);
                //label.l_xml.to_info.Add("to_zipcode",Request.Form["to_zipcode"]);
                //label.l_xml.to_info.Add("to_email",Request.Form["to_email"]);
                //label.l_xml.to_info.Add("to_telephone",Request.Form["to_telephone"]);
                //label.l_xml.to_info.Add("to_nation",Request.Form["to_nation"]);
                //label.l_xml.to_info.Add("to_city",Request.Form["to_city"]);
                //label.l_xml.to_info.Add("to_nation_state",Request.Form["to_nation_state"]);

                //label.l_xml.parcel_info.Add("pkg_length",Request.Form["pkg_length"]);
                //label.l_xml.parcel_info.Add("pkg_width",Request.Form["pkg_width"]);
                //label.l_xml.parcel_info.Add("pkg_height",Request.Form["pkg_height"]);
                //label.l_xml.parcel_info.Add("pkg_weight",Request.Form["pkg_weight"]);



                //label.lb_content=label.l_xml.generateXml().ToString();
                //label.l_xml=label.l_xml;
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
