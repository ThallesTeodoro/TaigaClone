using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Api.Features.Projects.ViewModels;
using Taiga.Api.Utilities;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace Taiga.Api.Features.Projects
{
    [ApiController]
    [Route("project")]
    public class ProjectController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _environment;

        public ProjectController(
            ILogger<ProjectController> logger,
            IUnitOfWork uow,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _uow = uow;
            _environment = environment;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] CreateProjectViewModel model)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                if (ModelState.IsValid)
                {
                    int userId = Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    User user = _uow.UserRepository.GetById(userId);

                    if (user != null)
                    {
                        Project project = new Project
                        {
                            OwnerId = user.Id,
                            Name = model.Name,
                            Description = model.Description
                        };

                        string slug = model.Name.ToLower().Replace(" ", "-");
                        slug = StringFormat.RemoveAccents(slug);

                        int slugEqualCount = _uow.ProjectRepository.CountBySlug(slug);

                        if (slugEqualCount > 0)
                        {
                            slug += "-" + (slugEqualCount + 1);
                        }

                        project.Slug = slug;

                        if (model.Image != null)
                        {
                            project.Image = FileManagement.SaveFile(
                                model.Image,
                                Path.Combine(_environment.WebRootPath, "img/projects")).ToString();
                        }

                        _uow.Commit();
                    }
                    else
                    {
                        response.StatusCode = 401;
                        response.Message = "Unauthorized.";
                    }
                }
                else
                {
                    response.StatusCode = 422;
                    response.Message = "Unprocessable Entity.";
                    response.Errors = Validations.FormatViewModelErrors(ModelState);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Project Create error: " + exception);
                _uow.Rollback();
                response.StatusCode = 500;
                response.Message = "Internal Server Error";
            }

            HttpContext.Response.StatusCode = response.StatusCode;
            return Json(response);
        }
    }
}
