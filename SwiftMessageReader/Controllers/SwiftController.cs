using Microsoft.AspNetCore.Mvc;

using SwiftMessageReader.Exceptions;
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

                SwiftLogger.Info(Messages.SuccessfulUpload);
                return Ok();
            }
            catch (InvalidFileException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (WrongBracketsSequence)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (WrongMessageStructure)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}