using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;
using MoviesApp.Filters;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;


        public ActorsController(MoviesContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(_context.Actors.ToList());
            return View(actors);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeFilter]
        public IActionResult Create([Bind("Id, FirstName, LastName, Birthday")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Movie>(inputModel));
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<EditActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, FirstName, LastName, Birthday")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = _mapper.Map<Actor>(editModel);
                    actor.Id = id;
                    _context.Update(actor);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!ActorExists(id))
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
            return View(editModel);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<DeleteActorViewModel>(_context.Actors.FirstOrDefault(m => m.Id == id));

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _context.Actors.Find(id);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            _logger.LogError($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(a => a.Id == id);
        }
    }
}