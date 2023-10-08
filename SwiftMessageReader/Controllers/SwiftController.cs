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
        {
            try
            {
                service.ManageFile(file);

                SwiftLogger.Info("File uploaded");

                return Ok();

            }
            catch (Exception ex)
            {
                SwiftLogger.Error("Error occures when trying to upload the file: " + ex);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}