using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.Models.LeaveType;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HR_Managment.MVC.Controllers
{
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            return View(leaveTypes);
        }


        public async Task<IActionResult> Details(int id)
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypeDetailsAsync(id);
            return View(leaveTypes);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveTypeVM model)
        {
            try
            {
                var response = await _leaveTypeService.CreateLeaveTypeAsync(model);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                response.ValidationErrors.ForEach(x => ModelState.AddModelError("", x));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeDetailsAsync(id);
            return View(leaveType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeVM model)
        {
            try
            {
                var response = await _leaveTypeService.UpdateLeaveTypeAsync(model);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                response.ValidationErrors.ForEach(x => ModelState.AddModelError("", x));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeDetailsAsync(id);
            return View(leaveType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,LeaveTypeVM model)
        {
            try
            {
                var response = await _leaveTypeService.DeleteLeaveTypeAsync(model.Id);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                response.ValidationErrors.ForEach(x => ModelState.AddModelError("", x));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }
    }
}
