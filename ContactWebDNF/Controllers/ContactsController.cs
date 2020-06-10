using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactWebDNF.Models;
using Microsoft.AspNet.Identity;

namespace ContactWebDNF.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string _userId;
        // GET: Contacts
        public ActionResult Index()
        {
            _userId = GetCurrentUserId(); //why do this, Just use the Function
            //limit contacts to only those for this user
            var contacts = db.Contacts.Include(c => c.State).Include(c => c.User).Where(x => x.UserId == _userId);
            return View(contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            _userId = GetCurrentUserId(); //why do this, Just use the Function

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            _userId = GetCurrentUserId();
            
            

            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            ViewBag.UserId = _userId;
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Phone,Birthday,Address1,Address2,City,Postcode,StateId,UserId")] Contact contact)
        {
            _userId = GetCurrentUserId(); 
            contact.UserId = _userId;
            //I changed the model to make Address2 not required. and this blew up the validation ModelState.IsValid was always false
            ModelState.Clear();
            TryValidateModel(contact); //fixes errors in the course material. Hopeless tacher
            if (ModelState.IsValid)
            {
                
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
            ViewBag.UserId = _userId;

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            _userId = GetCurrentUserId(); 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
            ViewBag.UserId = _userId;
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Phone,Birthday,Address1,Address2,City,Postcode,StateId")] Contact contact)
        {
            _userId = GetCurrentUserId(); //why do this, Just use the Function, cuz linq cant use the function
            
            contact.UserId = _userId;
            ModelState.Clear();
            TryValidateModel(contact); //fixes errors in the course material. Hopeless tacher
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
            
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            _userId = GetCurrentUserId(); //why do this, Just use the Function
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userId = GetCurrentUserId();
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
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

        protected string GetCurrentUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}
