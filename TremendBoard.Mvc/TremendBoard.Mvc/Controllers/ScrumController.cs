using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.ScrumViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Controllers
{
    public class ScrumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScrumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            var model = new BoardViewModel
            {
                ProjectId = id,
                ProjectName = project.Name,
                BacklogTasks = _unitOfWork.ProjectTask.GetProjectTasks(id, ProjectTaskStatus.Backlog.ToString()),
                InProgressTasks = _unitOfWork.ProjectTask.GetProjectTasks(id, ProjectTaskStatus.InProgress.ToString()),
                InTestTasks = _unitOfWork.ProjectTask.GetProjectTasks(id, ProjectTaskStatus.Test.ToString()),
                CompletedTasks = _unitOfWork.ProjectTask.GetProjectTasks(id, ProjectTaskStatus.Done.ToString())
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddTask(string id)
        {
            var model = new BoardAddTaskViewModel
            {
                ProjectId = id,
                Users = new List<ProjectUserDetailViewModel>()
            };

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetailViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });


            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = usersView.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = rolesView.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDetailViewModel
                {
                    ProjectId = id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.RoleName
                };

                model.Users.Add(projectUser);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTask(BoardAddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var project = await _unitOfWork.Project.GetByIdAsync(model.ProjectId);
            var user = await _unitOfWork.User.GetByIdAsync(model.UserId);

            var projectTask = new ProjectTask
            {
                Project = project,
                User = user,
                Content = model.Content,
                Duration = GetTimeSpanForHourAndMinutes(model.Hour, model.Minute),
                Status = ProjectTaskStatus.Backlog.ToString()
            };

            await _unitOfWork.ProjectTask.AddAsync(projectTask);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index), new { project.Id });
        }
        
        public async Task<IActionResult> StartTask(string id)
        {
            var tasks = await _unitOfWork.ProjectTask.GetAsync(x => x.Id == id, null, "Project");
            var task = tasks.FirstOrDefault();
            task.Status = ProjectTaskStatus.InProgress.ToString();

            _unitOfWork.ProjectTask.Update(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        public async Task<IActionResult> VerifyTask(string id)
        {
            var tasks = await _unitOfWork.ProjectTask.GetAsync(x => x.Id == id, null, "Project");
            var task = tasks.FirstOrDefault();
            task.Status = ProjectTaskStatus.Test.ToString();

            _unitOfWork.ProjectTask.Update(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        public async Task<IActionResult> CompleteTask(string id)
        {
            var tasks = await _unitOfWork.ProjectTask.GetAsync(x => x.Id == id, null, "Project");
            var task = tasks.FirstOrDefault();
            task.Status = ProjectTaskStatus.Done.ToString();

            _unitOfWork.ProjectTask.Update(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        public async Task<IActionResult> RemoveTask(string id)
        {
            var task = await _unitOfWork.ProjectTask.GetByIdAsync(id);

            _unitOfWork.ProjectTask.Remove(task);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        private static TimeSpan GetTimeSpanForHourAndMinutes(int hours, int minutes)
        {
            int days = 0;
            int remainingHours;
            int remainingMinutes;

            if (minutes > 59)
            {
                remainingMinutes = minutes % 60;
                hours += minutes / 60;
            }
            else
            {
                remainingMinutes = minutes;
            }

            if (hours > 8)
            {
                days = hours / 8;
                remainingHours = hours % 8;
            }
            else
            {
                remainingHours = hours;
            }

            return new TimeSpan(days, remainingHours, remainingMinutes, 0);
        }
    }
}