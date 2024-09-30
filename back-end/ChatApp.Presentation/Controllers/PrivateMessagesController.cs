using ChatApp.Business.Services.PrivateMessageServices.Interfaces;
using ChatApp.Business.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers
{
    [Route("api/private-messages")]
    [ApiController]
    public class PrivateMessagesController : ControllerBase
    {
        private readonly IPrivateMessageService privateMessageService;
        private readonly IAuthenticatedUserService authenticatedUserService;
        public PrivateMessagesController(IPrivateMessageService privateMessageService)
        {
            this.privateMessageService = privateMessageService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPrivateMessages(
            DateTime? pageDate,
            int pageSize,
            int firstUserId,
            int secoundUserId)
        {
            var result = await privateMessageService.GetPrivateMessages(pageDate, pageSize, firstUserId, secoundUserId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("delete-message/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var result = await privateMessageService.DeletePrivateMessage(id);
            return Ok(result);
        }
    }
}
