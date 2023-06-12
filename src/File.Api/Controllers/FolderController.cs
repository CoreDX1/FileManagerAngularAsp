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
        public async Task<IActionResult> Get([FromBody] FolderRequestDto folder)
        {
            var response = await this.app.CreateFolder(folder);
            return StatusCode(200, response);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchFolder([FromBody] FolderRequestDto folder)
        {
            var response = await this.app.GetByName(folder);
            return StatusCode(200, response);
        }

        [HttpGet]
        [Route("View/{*path}")]
        public IActionResult View(string path)
        {
            var response = this.app.GetRoot(path);
            return StatusCode(200, response);
        }

        [HttpDelete]
        [Route("folder/delete")]
        public async Task<IActionResult> Delete([FromBody] FolderRequestDto folder)
        {
            var response = await this.app.Delete(folder);
            return StatusCode(200, response);
        }
    }
}
