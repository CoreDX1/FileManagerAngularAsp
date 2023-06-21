using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.DTO.Response.Folder;
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

        [HttpGet] // GET : api/folder/view/File/details
        [Route("view/{user}/{*file}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(BaseResponse<RootResponseDto>)
        )]
        public ActionResult<BaseResponse<RootResponseDto>> ViewFiles(string user, string file)
        {
            var response = this.app.GetRoot(user, file);
            return Ok(response);
        }

        [HttpGet]
        [Route("view/{user}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(BaseResponse<RootResponseDto>)
        )]
        public ActionResult<BaseResponse<RootResponseDto>> ViewFiles(string user)
        {
            var response = this.app.CloneGetRoot(user);
            return Ok(response);
        }

        [HttpGet] // GET : api/folder/searchbycontent/AAA
        [Route("searchByContent/{*path}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(BaseResponse<IEnumerable<FolderResponseDto>>)
        )]
        public async Task<IActionResult> SearchFilesContent(string path)
        {
            var response = await this.app.SearchByContent(path);
            return Ok(response);
        }

        [HttpPost] // POST : api/folder/create
        [Route("create")]
        public async Task<IActionResult> CreateFolder([FromBody] FolderCreateRequestDto folder)
        {
            var response = await this.app.CreateFolder(folder);
            return Ok(response);
        }

        [HttpPost] // POST : api/folder/view/search
        [Route("view/search")]
        public async Task<IActionResult> SearchFolder([FromBody] FolderRequestDto folder)
        {
            var response = await this.app.GetByName(folder.Name!, folder.Path!);
            return Ok(response);
        }

        [HttpDelete] // DELETE : api/folder/delete
        [Route("delete")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(BaseResponse<IEnumerable<FolderResponseDto>>)
        )]
        public async Task<IActionResult> Delete([FromBody] FolderRequestDto folder)
        {
            var response = await this.app.Delete(folder);
            return Ok(response);
        }
    }
}
