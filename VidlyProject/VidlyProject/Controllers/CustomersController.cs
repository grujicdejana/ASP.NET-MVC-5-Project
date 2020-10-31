using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VidlyProject.Models;

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