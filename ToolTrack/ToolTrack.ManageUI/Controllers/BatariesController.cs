using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using ToolTrack.Repository;
using Core;
using System.Linq;
using System.Collections.Generic;

namespace ToolTrack.ManageUI.Controllers
{
    public class BatariesController : Controller
    {
        private readonly BaseInterface<Batary> _bataryRepository;
        private readonly BaseInterface<BataryModel> _bataryModelRepository;
        private readonly BaseInterface<Condition> _conditionRepository;
        private readonly BaseInterface<Worker> _workerRepository;
        private readonly BaseInterface<Location> _locationRepository;

        public BatariesController(
            BaseInterface<Batary> bataryRepository,
            BaseInterface<BataryModel> bataryModelRepository,
            BaseInterface<Condition> conditionRepository,
            BaseInterface<Worker> workerRepository,
            BaseInterface<Location> locationRepository)
        {
            _bataryRepository = bataryRepository;
            _bataryModelRepository = bataryModelRepository;
            _conditionRepository = conditionRepository;
            _workerRepository = workerRepository;
            _locationRepository = locationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var bataries = await _bataryRepository.GetAsync();
            return View(bataries);
        }

        public async Task<IActionResult> Details(int id)
        {
            var batary = await _bataryRepository.GetAsync(id);
            if (batary == null) return NotFound();
            return View(batary);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batary batary)
        {
            if (!ModelState.IsValid)
            {
                await PopulateViewData();
                return View(batary);
            }

            await _bataryRepository.CreateAsync(batary);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var batary = await _bataryRepository.GetAsync(id);
            if (batary == null) return NotFound();

            await PopulateViewData();
            return View(batary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Batary batary)
        {
            if (id != batary.Id) return NotFound();

            var existingBatary = await _bataryRepository.GetAsync(id);
            if (existingBatary == null) return NotFound(); // Додаткова перевірка

            if (!ModelState.IsValid)
            {
                await PopulateViewData();
                return View(batary);
            }

            await _bataryRepository.UpdateAsync(batary);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var batary = await _bataryRepository.GetAsync(id);
            if (batary == null) return NotFound();

            return View(batary);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batary = await _bataryRepository.GetAsync(id);
            if (batary == null) return NotFound(); // Додаткова перевірка

            await _bataryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task PopulateViewData()
        {
            ViewData["BataryModelId"] = new SelectList(await _bataryModelRepository.GetAsync() ?? new List<BataryModel>(), "Id", "Name");
            ViewData["ConditionId"] = new SelectList(await _conditionRepository.GetAsync() ?? new List<Condition>(), "Id", "Name");
            ViewData["LastWorkerId"] = new SelectList(await _workerRepository.GetAsync() ?? new List<Worker>(), "Id", "Email");
            ViewData["LastLocationId"] = new SelectList(await _locationRepository.GetAsync() ?? new List<Location>(), "Id", "Name");
        }
    }
}
