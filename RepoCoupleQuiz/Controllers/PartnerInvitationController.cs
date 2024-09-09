using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
    public class PartnerInvitationController : BaseController
    {
        private readonly PartnerInvitationService partnerInvitationService;
        public PartnerInvitationController(PartnerInvitationService partnerInvitationService)
        {
            this.partnerInvitationService = partnerInvitationService;
        }
        [Authorize]
        [HttpPost("GenerateCode")]
        public async Task<IActionResult> GenerateCode([FromBody] GenerateCodeRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await partnerInvitationService.GenerateCode(request),
                    Message = "Code generated SuccessFully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("PartnerJoining")]
        public async Task<IActionResult> PartnerJoin([FromBody] PartnerInvitationJoiningRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await partnerInvitationService.PartnerJoining(request),
                    Message = "Partner Join SuccessFully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllSessions")]
        public async Task<IActionResult> GetAllSession()
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await partnerInvitationService.GetAllSessionsAsync(),
                    Message = "All Session Listed SuccessFully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        


    }

}

