using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VidlyProject.Models;
using VidlyProject.ViewModels;
using System.Data.Entity.Validation;

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

            //var movies = _context.Movies.Include(c => c.Genre).ToList();

            //return View(movies);

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult MovieForm()
        {
            var genreTypes = _context.Genres.ToList();

            var viewModel = new MovieViewModel
            {
                Genres = genreTypes
            };

            ViewBag.Title = "New Movie";

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieById = _context.Movies.Where(c => c.Id == movie.Id).SingleOrDefault();
                movieById.Name = movie.Name;
                movieById.ReleaseDate = movie.ReleaseDate;
                movieById.DateAdded = movie.DateAdded;
                movieById.GenreId = movie.GenreId;
                movieById.NumberInStock = movie.NumberInStock;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("GetMovies", "Movies");
        }

        [HttpGet]
        public ActionResult Details(int idMovie)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == idMovie);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int idMovie)
        {
            var movie = _context.Movies.Where(c => c.Id == idMovie).FirstOrDefault();

            if(movie == null)        
                return HttpNotFound();           

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            ViewBag.Title = "Edit Movie";

            return View("MovieForm", viewModel);
        }
    }
}