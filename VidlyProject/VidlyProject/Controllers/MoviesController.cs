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
    public class MoviesController : Controller
    {
        
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        

        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetMovies()
        {
            /*var movies = new List<Movie>
            {
                new Movie {Name = "Shrek"},
                new Movie {Name = "Wall-e"}
            };*/

            var movies = _context.Movies.Include(c => c.Genre).ToList();

            return View(movies);
        }

        [HttpGet]
        public ActionResult Details(int idMovie)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == idMovie);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        /*public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }*/
    }
}