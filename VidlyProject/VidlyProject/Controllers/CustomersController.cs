using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VidlyProject.Models;
using VidlyProject.ViewModels;

namespace VidlyProject.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        [HttpGet]
        public ActionResult Index()
        {
            /*List<Customer> customers = GetCustomers();*/

            //na ovaj nacin preuzimamo sve customer iz baze

            
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); //include membership type kako bi se prikazalo na webu, jer je membership type referencirajuca klasa
            return View(customers);
        }

        //Add Customer
        [HttpGet]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                /*Customer = new Customer() je dodato, jer nakon sto je u View 
                 dodati ValidationSummary izbacice i error za Id koji je dodat kao hidden,
                 ali kada se pogleda inspect vidi se da value nema vrijednost, posmatra se kao 
                 prazan string, a MVC framework ne zna kako da prevede prazan string u int.
                 Zato kada napravimo objekat Customer, njegovi parametri dobiju default-ne vr.
                 Tako id dobije vr. nula i njegov error nece biti ispisan*/
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");

        }

        [HttpGet]
        public ActionResult Edit(int idCustomer)
        {
            var customer = _context.Customers.Where(c => c.Id == idCustomer).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpGet]
        public ActionResult Details(int idCustomer)
        {

            /*var customer = GetCustomerById(idCustomer);

            if(customer.Name != null)
            {
                return View(customer);
            }
            else
            {
                return HttpNotFound();
            }*/

            
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == idCustomer);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        /*private Customer GetCustomerById(int id)
        {
            List<Customer> customers = GetCustomers();

            var customer = new Customer();

            foreach(var cust in customers)
            {
                if (cust.Id == id)
                {
                    customer = cust;
                    break;
                }
            }

            return customer;
        }*/

        /*private List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Smith"},
                new Customer {Id = 2, Name = "Mary Williams"}
            };

            return customers;
        }*/
    }
}