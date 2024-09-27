using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP2.Models;

namespace TP2.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationdbContext _context;

        public GenreController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: Genre
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.Include(m => m.Movies).ToListAsync());
        }

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .Include(m=>m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genre/Create
        // GET: Genre/Create
        public IActionResult Create()
        {
            var viewModel = new GenreViewModel
            {
                Movies = _context.Movies.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = viewModel.Name,
                    Movies = _context.Movies.Where(m => viewModel.SelectedMovieIds.Contains(m.Id)).ToList()
                };
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
            viewModel.Movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
            return View(viewModel);
        }

        // GET: Genre/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            var viewModel = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                SelectedMovieIds = genre.Movies.Select(m => m.Id).ToList(),
                Movies = _context.Movies.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GenreViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var genre = await _context.Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == id);
                if (genre == null)
                {
                    return NotFound();
                }

                genre.Name = viewModel.Name;
                genre.Movies = _context.Movies.Where(m => viewModel.SelectedMovieIds.Contains(m.Id)).ToList();

                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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

            viewModel.Movies = _context.Movies.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
            return View(viewModel);
        }
        

        // GET: Genre/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(Guid id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}