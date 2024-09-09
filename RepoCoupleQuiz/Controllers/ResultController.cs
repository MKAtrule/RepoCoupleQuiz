using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
    public class ResultController : BaseController
    {
        private readonly ResultService resultService;
        public ResultController(ResultService resultService)
        {
            this.resultService = resultService;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetSessionResultsByPartnerInvitation")]
        public async Task<IActionResult> GetAllSession(Guid id)
        {
            try
            {
          
                return new JsonResult(new
                {
                    success = true,
                    data = await resultService.GetAllResultByPartnerInvitationId(id),
                    Message = "All Session related to this partner Invitation Listed SuccessFully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new {  Messsage = ex.Message });
            }
        }
        [HttpGet("GetTotalScoresByPartnerInvitation")]
        public async Task<IActionResult> GetAllScors(Guid id)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await resultService.CalculateScoresForPartnerInvitation(id),
                    Message = "All Scores For both users related to this partner Invitation Listed SuccessFully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }



    }
}
