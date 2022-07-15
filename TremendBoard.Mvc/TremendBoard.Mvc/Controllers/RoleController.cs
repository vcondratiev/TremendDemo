using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models.RoleViewModels;

namespace TremendBoard.Mvc.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles.OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ApplicationRoleIndexViewModel
            {
                Roles = rolesView
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRoleDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _unitOfWork.Role.AddAsync(new ApplicationRole
            {
                Name = model.RoleName,
                Description = model.Description,
                Status = true
            });

            var createResult = await _unitOfWork.SaveAsync();

            if (createResult == 0)
            {
                throw new ApplicationException($"Unexpected error occurred during creating the role .");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _unitOfWork.Role.GetByIdAsync(id);
            
            if (role == null)
            {
                StatusMessage = "Role not found";
                return View();
            }

            var model = new ApplicationRoleDetailViewModel
            {
                Id = id,
                RoleName = role.Name,
                Description = role.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRoleDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var roleId = model.Id;

            var role = await _unitOfWork.Role.GetByIdAsync(roleId);
            
            if (role == null)
            {
                StatusMessage = "Role not found";
                return View();
            }

            role.Name = model.RoleName;
            role.Description = model.Description;

            _unitOfWork.Role.Update(role);
            var updateResult = await _unitOfWork.SaveAsync();

            if (updateResult == 0)
            {
                throw new ApplicationException($"Unexpected error occurred during updating the role .");
            }

            model.StatusMessage = $"{role.Name} role has been updated";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _unitOfWork.Role.GetByIdAsync(id);
            if (role == null)
            {
                StatusMessage = "Role not found";
                return View();
            }

            var model = new ApplicationRoleDetailViewModel
            {
                Id = id,
                RoleName = role.Name,
                Description = role.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApplicationRoleDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var roleId = model.Id;

            var role = await _unitOfWork.Role.GetByIdAsync(roleId);
            if (role == null)
            {
                StatusMessage = "Role not found";
                return View();
            }

            _unitOfWork.Role.Remove(role);
            var updateResult = await _unitOfWork.SaveAsync();

            if (updateResult == 0)
            {
                throw new ApplicationException($"Unexpected error occurred during deleting the role .");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}