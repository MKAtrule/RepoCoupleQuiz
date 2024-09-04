using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
 //    [Authorize]
    public class SessionHistoryController : BaseController
    {
        private readonly SessionHistoryService sessionHistoryService;
        public SessionHistoryController(SessionHistoryService sessionHistoryService)
        {
            this.sessionHistoryService = sessionHistoryService;
        }
      
        [HttpGet("GetSessionHistory")]
        public async Task<IActionResult> GetUnAttemptedQuestionForUsers(Guid id)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await sessionHistoryService.GetUnAttemptedSessionsForUser(id),
                    Message = "Session History Listed Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
      
        [HttpPost("HandleUnAttemptedQuiz")]
        public async Task<IActionResult> HandleUnAttemptedSessionQuiz([FromBody]SessionHistoryRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await sessionHistoryService.HandleSessionHistoryAnswer(request),
                    Message = "Result Listed Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
    }
}
