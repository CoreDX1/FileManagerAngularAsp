using File.Application.DTO.Request.Folder;
using File.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace src.File.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderApplication app;

        public FolderController(IFolderApplication app)
        {
            this.app = app;
        }

        [HttpPost]
        [Route("folder")]
        public async Task<IActionResult> Get(FolderRequestDto folder)
        {
            var response = await this.app.CreateFolder(folder);
            return StatusCode(200, response);
        }
    }
}
