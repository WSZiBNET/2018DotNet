using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DBContext _context;

        public MoviesController(DBContext context)
        {
            _context = context;
        }



        // GET: Movies
        public async Task<IActionResult> Index(string searchMovieName, string searchGenreName, string searchAuthorName)
        {
            if (searchMovieName != null)
            {
                var movies = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) where m.Title.Contains(searchMovieName) select m).ToList();
                return View(movies);
            }

            if (searchGenreName != null)
            {

                int genreId = (from g in _context.Genres where g.Name.Contains(searchGenreName) select g.GenreId).SingleOrDefault();

                var genres = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) where m.GenreId.Equals(genreId) select m).ToList();
                return View(genres);
            }

            if (searchAuthorName != null)
            {

                int authorId = (from a in _context.Author where a.Name.Contains(searchAuthorName) && a.Surname.Contains(searchAuthorName) select a.AuthorId).SingleOrDefault();

                var authors = (from a in _context.Movie.Include(a => a.Author).Include(a => a.Genres) where a.GenreId.Equals(authorId) select a).ToList();
                return View(authors);
            }

            ViewBag.serach = searchMovieName;
            var dBContext = _context.Movie.Include(m => m.Author).Include(m => m.Genres);
            return View(await dBContext.ToListAsync());
            //var movies = from m in _context.Movie
            //             select m;

            //if (!String.IsNullOrEmpty(id))
            //{
            //    movies = movies.Where(s => s.Title.Contains(id));
            //}

            //return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Author)
                .Include(m => m.Genres)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId","FullName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ProductionDate,MovieDescription,GenreId,AuthorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ProductionDate,MovieDescription,GenreId,AuthorId")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Author)
                .Include(m => m.Genres)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
