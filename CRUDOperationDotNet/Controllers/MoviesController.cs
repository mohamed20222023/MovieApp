using CRUDOperationDotNet.Models;
using CRUDOperationDotNet.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace CRUDOperationDotNet.Controllers
{
    public class MoviesController : Controller
    {

        private readonly ApplicationDbContext _context;
        //private readonly IToastNotification _toastNotification;

        public MoviesController(ApplicationDbContext context /*IToastNotification toastNotification*/)
        {
            _context = context;
            //_toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.OrderByDescending(m => m.Rate).ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var ViewModel = new MovieFormVM() {

                Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync(),
            };

            return View("MovieForm",ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormVM model)
        {
            
            // Step 01 - Catch Folder Path
            string imagePath = Directory.GetCurrentDirectory() + @"\wwwroot\files\images";

            // Step 02 - Catch File Name & Generate GUID to Generate Token
            string imageName = Guid.NewGuid()+Path.GetFileName(model.Poster.FileName);

            // Step 03 - Merge Path + Name 
            string FinalImagePath = Path.Combine(imagePath, imageName);

            // Step 04 - Save Your File 
            using (var stream = new FileStream(FinalImagePath , FileMode.Create))
            {
                model.Poster.CopyTo(stream);
            }

            model.PosterName = imageName;

            var movie = new Movies
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Year = model.Year,
                Rate = model.Rate,
                Storyline = model.Storyline,
                Poster = model.PosterName
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            //_toastNotification.AddSuccessToastMessage("Movie Added Successfully");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var movie = await _context.Movies.FindAsync(id);
            if( movie == null)
                return NotFound();

            var ViewModel = new MovieFormVM()
            {
                Id = movie.Id,
                GenreId= movie.GenreId,
                Rate= movie.Rate,
                Storyline= movie.Storyline,
                Title= movie.Title,
                Year= movie.Year,
                PosterName = movie.Poster,
                Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync(),
            };

            return View("MovieForm", ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieFormVM model)
        {
            var movie = await _context.Movies.FindAsync(model.Id);
            if (movie == null)
                return NotFound();


            // Step 01 - Catch Folder Path
            string imagePath = Directory.GetCurrentDirectory() + @"\wwwroot\files\images";

            // Step 02 - Catch File Name & Generate GUID to Generate Token
            string imageName = Guid.NewGuid() + Path.GetFileName(model.Poster.FileName);

            // Step 03 - Merge Path + Name 
            string FinalImagePath = Path.Combine(imagePath, imageName);

            // Step 04 - Save Your File 
            using (var stream = new FileStream(FinalImagePath, FileMode.Create))
            {
                model.Poster.CopyTo(stream);
            }

            model.PosterName = imageName;




            movie.Title = model.Title;
            movie.Year = model.Year;
            movie.Storyline = model.Storyline;
            movie.GenreId = model.GenreId;
            movie.Rate = model.Rate;
            movie.Poster = model.PosterName;

 
            _context.SaveChanges();
            //_toastNotification.AddSuccessToastMessage("Movie Updated Successfully");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
