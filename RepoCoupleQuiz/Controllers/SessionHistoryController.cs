using Microsoft.AspNetCore.Mvc;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
    public class SessionHistoryController : BaseController
    {
        private readonly SessionHistoryService sessionHistoryService;
        public SessionHistoryController(SessionHistoryService sessionHistoryService)
        {
            this.sessionHistoryService = sessionHistoryService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnAttemptedQuestionForUsers(Guid id)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await sessionHistoryService.GetUnAttemptedSessionsForUser(id),
                    Message = "Question Listed Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
    }
}
