using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.Models.LeaveRequest;
using HR_Managment.MVC.Models.LeaveType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace HR_Managment.MVC.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, ILeaveTypeService leaveTypeService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveTypeService = leaveTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var leaveRequests = await _leaveRequestService.GetAllLeaveRequestAsync();
            return View(leaveRequests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestDetailsAsync(id);
            return View(leaveRequest);
        }

        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestVM model)
        {
            try
            {
                var response = await _leaveRequestService.CreateLeaveRequestAsync(model);
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
            var leaveRequest = await _leaveRequestService.GetLeaveRequestDetailsAsync(id);
            var leaveTypesList = await _leaveTypeService.GetAllLeaveTypesAsync();
            var selectedList = leaveTypesList.Select(l => new SelectListItem(l.Name, l.Id.ToString())).ToList();
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i].Value == leaveRequest.LeaveTypeId.ToString())
                    selectedList[i].Selected = true;
            }
            ViewData["SelectedList"] = selectedList;
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeaveRequestVM leaveRequest)
        {
            try
            {
                var response = await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest);
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
            var leaveTypesList = await _leaveTypeService.GetAllLeaveTypesAsync();
            var selectedList = leaveTypesList.Select(l => new SelectListItem(l.Name, l.Id.ToString())).ToList();
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i].Value == leaveRequest.LeaveTypeId.ToString())
                    selectedList[i].Selected = true;
            }
            ViewData["SelectedList"] = selectedList;
            return View(leaveRequest);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestDetailsAsync(id);
            ViewBag.LeaveType = (await _leaveTypeService.GetLeaveTypeDetailsAsync(leaveRequest.LeaveTypeId)).Name;
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, LeaveRequestVM model)
        {
            try
            {
                var response = await _leaveRequestService.DeleteLeaveRequestAsync(model.Id);
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
