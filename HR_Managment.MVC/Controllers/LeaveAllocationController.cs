using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.Models.LeaveAllocation;
using HR_Managment.MVC.Models.LeaveRequest;
using HR_Managment.MVC.Models.LeaveType;
using HR_Managment.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_Managment.MVC.Controllers
{
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveAllocationController(ILeaveAllocationService leaveAllocationService, ILeaveTypeService leaveTypeService)
        {
            _leaveAllocationService = leaveAllocationService;
            _leaveTypeService = leaveTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var leaveAllocations = await _leaveAllocationService.GetAllLeaveAllocationsAsync();
            return View(leaveAllocations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var leaveAllocation = await _leaveAllocationService.GetLeaveAllocationDetailsAsync(id);
            return View(leaveAllocation);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveAllocationVM model)
        {
            try
            {
                var response = await _leaveAllocationService.CreateLeaveAllocationAsync(model);
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
            var leaveAllocation = await _leaveAllocationService.GetLeaveAllocationDetailsAsync(id);
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            var selectList = new SelectList(leaveTypes, nameof(LeaveTypeVM.Id), nameof(LeaveTypeVM.Name),
                leaveAllocation.LeaveTypeId.ToString());
            ViewBag.LeaveTypes = selectList;
            return View(leaveAllocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeaveAllocationVM leaveAllocation)
        {
            try
            {
                var response = await _leaveAllocationService.UpdateLeaveAllocationAsync(leaveAllocation);
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
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            var selectList = new SelectList(leaveTypes, nameof(LeaveTypeVM.Id), nameof(LeaveTypeVM.Name),
                leaveAllocation.LeaveTypeId.ToString());
            ViewBag.LeaveTypes = selectList;
            return View(leaveAllocation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaveAllocation = await _leaveAllocationService.GetLeaveAllocationDetailsAsync(id);
            ViewBag.LeaveType = (await _leaveTypeService.GetLeaveTypeDetailsAsync(leaveAllocation.LeaveTypeId)).Name;
            return View(leaveAllocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, LeaveRequestVM model)
        {
            try
            {
                var response = await _leaveAllocationService.DeleteLeaveAllocationAsync(model.Id);
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

            ViewBag.LeaveType = (await _leaveTypeService.GetLeaveTypeDetailsAsync(model.LeaveTypeId)).Name;
            return View(model);
        }

    }
}
