using Jenkins.API.Context;
using Jenkins.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jenkins.API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _projectContext;

        public ProjectController(AppDbContext projectContext)
        {
            _projectContext = projectContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var projects = await _projectContext.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProject([FromBody] Project projectObj)
        {
            if (projectObj == null)
                return BadRequest();

            await _projectContext.Projects.AddAsync(projectObj);
            await _projectContext.SaveChangesAsync();

            return Ok(new { Message = "Project added successfully!" });

        }
    }
}
