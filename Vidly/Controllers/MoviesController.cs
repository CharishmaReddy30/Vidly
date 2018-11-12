using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Genre).ToList();
            if (!User.IsInRole("CanManageMovies"))
                return View("ReadOnlyList", movies);
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = db.Movies.Include(m => m.Genre).Where(m => m.Id == id).SingleOrDefault();
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        public ActionResult New()
        {
            var model = new MovieFormViewModel()
            {
                //Movie = new Movie(),
                Genres = db.Genres.ToList()
            };
            return View("MovieForm", model);
        }
        public ActionResult Edit(int id)
        {
            var movie = db.Movies.Where(m => m.Id == id).SingleOrDefault();
            if (movie == null)
                return HttpNotFound();
            var model = new MovieFormViewModel(movie)
            {
                Genres = db.Genres.ToList()
            };
            return View("MovieForm", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveForm(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var errorModel = new MovieFormViewModel(movie)
                {
                    //Movie = model.Movie,
                    Genres = db.Genres.ToList()
                };
                return View("MovieForm", errorModel);
            }
            if(movie.Id != 0)
            {
                var newmovie = db.Movies.Where(m => m.Id == movie.Id).SingleOrDefault();
                newmovie.Name = movie.Name;
                newmovie.ReleaseDate = movie.ReleaseDate;
                newmovie.NumberInStock = movie.NumberInStock;
                newmovie.GenreId = movie.GenreId;
            }
            else
            {
                var newmovie = new Movie()
                {
                    Name = movie.Name,
                    ReleaseDate = movie.ReleaseDate,
                    DateAdded = DateTime.Now,
                    NumberInStock = movie.NumberInStock,
                    GenreId = movie.GenreId
                };
                db.Movies.Add(newmovie);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}