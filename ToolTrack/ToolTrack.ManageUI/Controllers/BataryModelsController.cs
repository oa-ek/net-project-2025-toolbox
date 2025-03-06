using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolTrack.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ToolTrack.ManageUI.Controllers
{
    public class BataryModelsController : Controller
    {
        private readonly BaseInterface<BataryModel> _bataryModelRepository;
        private readonly BaseInterface<Brand> _brandRepository;

        public BataryModelsController(
            BaseInterface<BataryModel> bataryModelRepository,
            BaseInterface<Brand> brandRepository)
        {
            _bataryModelRepository = bataryModelRepository;
            _brandRepository = brandRepository;
        }

        // GET: BataryModels
        public async Task<IActionResult> Index()
        {
            var bataryModels = await _bataryModelRepository.GetAsync();
            return View(bataryModels);
        }

        // GET: BataryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bataryModel = await _bataryModelRepository.GetAsync(id.Value);
            if (bataryModel == null)
            {
                return NotFound();
            }

            return View(bataryModel);
        }

        // GET: BataryModels/Create
        public async Task<IActionResult> Create()
        {
            var brands = await _brandRepository.GetAsync();
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name");
            return View();
        }

        // POST: BataryModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BrandId")] BataryModel bataryModel)
        {
            if (ModelState.IsValid)
            {
                await _bataryModelRepository.CreateAsync(bataryModel);
                return RedirectToAction(nameof(Index));
            }
            var brands = await _brandRepository.GetAsync();
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name", bataryModel.BrandId);
            return View(bataryModel);
        }

        // GET: BataryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bataryModel = await _bataryModelRepository.GetAsync(id.Value);
            if (bataryModel == null)
            {
                return NotFound();
            }
            var brands = await _brandRepository.GetAsync();
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name", bataryModel.BrandId);
            return View(bataryModel);
        }

        // POST: BataryModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BrandId")] BataryModel bataryModel)
        {
            if (id != bataryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bataryModelRepository.UpdateAsync(bataryModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _bataryModelRepository.GetAsync(bataryModel.Id) == null)
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
            var brands = await _brandRepository.GetAsync();
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name", bataryModel.BrandId);
            return View(bataryModel);
        }

        // GET: BataryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bataryModel = await _bataryModelRepository.GetAsync(id.Value);
            if (bataryModel == null)
            {
                return NotFound();
            }

            return View(bataryModel);
        }

        // POST: BataryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bataryModelRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
