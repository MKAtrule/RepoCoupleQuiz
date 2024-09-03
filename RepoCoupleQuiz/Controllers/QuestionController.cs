using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly QuestionService questionService;
        public QuestionController(QuestionService questionService)
        {
            this.questionService = questionService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] QuestionRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await questionService.CreateAsync(request),
                    Message = "Question Created Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetQuestionById")]
        public async Task<IActionResult> GetQuestionById( Guid id)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await questionService.GetQuestionById(id),
                    Message = "Question Listed Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody]UpdateQuestionRequestDTO request)
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await questionService.UpdateAsync(request),
                    Message = "Question Update Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }
        //[Authorize]
        [HttpGet("DailyQuestion")]
        public async Task<IActionResult> SendDailyQuestion()
        {
            try
            {

                return new JsonResult(new
                {
                    success = true,
                    data = await questionService.SendDailyQuestionAsync(),
                    Message = "Question Send Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Messsage = ex.Message });
            }
        }

    }
}
