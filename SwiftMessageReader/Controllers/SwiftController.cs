using Microsoft.AspNetCore.Mvc;

using SwiftMessageReader.Helpers;
using SwiftMessageReader.Services.Interfaces;

namespace SwiftReader.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class SwiftController : ControllerBase
    {
        private ISwiftService service;

        public SwiftController(ISwiftService service)
        {
            this.service = service;
        }

        [HttpPost("insert")]
        public IActionResult Insert(IFormFile file)
        {// TODO: Add appropriate status codes.
            try
            {
                service.ManageFile(file);

                SwiftLogger.Info(Messages.SuccessfulUpload);
                return Ok();
            }
            catch (Exception ex)
            {
                SwiftLogger.Error(Messages.UploadFailedError + ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}