using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using selfCoding.Areas.Admin.Models;
using selfCoding.Models;

namespace selfCoding.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shop
        public ActionResult Index(string category)
        {
            var products = db.Products.Include(p => p.Category).Where(p => p.Category.Name == category);
            return View(products.ToList());
        }
        

        public ActionResult AddToCart(int id)
        {
            int cartId = 1;

            Cart cart;

            if (db.Carts.Where(c=>c.Id==cartId).Any())
            {
                cart = db.Carts.Where(c => c.Id == cartId).Include(c => c.Items).First();
            }
            else
            {
                cart = new Cart();
                db.Carts.Add(cart);
            }

            CartItem cartItem;

            if (cart.Items.Any(i => i.ProductId == id))
            {
                cartItem = cart.Items.Where(i => i.ProductId == id).First();
                cartItem.Quantity += 1;
            }
            else
            {
                CartItem newItem = new CartItem();
                newItem.ProductId = id;
                newItem.Quantity = 1;
                cart.Items.Add(newItem);
            }

            db.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            int cartId = 1;

            Cart cart;

            if (db.Carts.Where(c => c.Id == cartId).Any())
            {
                cart = db.Carts.Where(c => c.Id == cartId).Include("Items.Product").First();
            }
            else
            {
                cart = new Cart();
                db.Carts.Add(cart);
            }
            var cartVM = new CartVM();
            cartVM.CartItems = cart.Items;

            cartVM.Total = cart.Items.Sum(i => i.Quantity*i.Product.Price);
            return View("Cart", cartVM);

        }
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }

            cartItem.Quantity++;

            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }

            cartItem.Quantity--;

            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public ActionResult CheckOut()
        {
            return View("PayPal"); //if this was called Checkout the "PayPal" would not be needed
        }
        // GET: Shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }


            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Products()
        {
            ViewBag.Message = "product page";



            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }
    }
}
