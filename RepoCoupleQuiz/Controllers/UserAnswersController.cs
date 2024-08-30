using Microsoft.AspNetCore.Mvc;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
    public class UserAnswersController:BaseController
    {
        private readonly UserAnswersService userAnswersService;
        public UserAnswersController(UserAnswersService userAnswersService)
        {
            this.userAnswersService = userAnswersService;
        }
        [HttpPost("Result")]
        public async Task<IActionResult> Create([FromBody] UserAnswersRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await userAnswersService.AddUserAnswersandResultAsync(request),
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
