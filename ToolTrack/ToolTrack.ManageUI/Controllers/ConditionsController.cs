using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core;
using ToolTrack.Repository;

namespace ToolTrack.ManageUI.Controllers
{
    public class ConditionsController : Controller
    {
        private readonly BaseInterface<Condition> _conditionRepository;

        public ConditionsController(BaseInterface<Condition> conditionRepository)
        {
            _conditionRepository = conditionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var conditions = await _conditionRepository.GetAsync();
            return View(conditions);
        }

        public async Task<IActionResult> Details(int id)
        {
            var condition = await _conditionRepository.GetAsync(id);
            if (condition == null) return NotFound();
            return View(condition);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Condition condition)
        {
            if (!ModelState.IsValid)
            {
                return View(condition);
            }

            await _conditionRepository.CreateAsync(condition);
            TempData["SuccessMessage"] = "Condition successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var condition = await _conditionRepository.GetAsync(id);
            if (condition == null) return NotFound();
            return View(condition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Condition condition)
        {
            if (id != condition.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(condition);
            }

            await _conditionRepository.UpdateAsync(condition);
            TempData["SuccessMessage"] = "Condition successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var condition = await _conditionRepository.GetAsync(id);
            if (condition == null) return NotFound();
            return View(condition);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _conditionRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Condition successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
