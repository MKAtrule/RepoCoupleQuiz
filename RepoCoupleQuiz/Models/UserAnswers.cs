﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class UserAnswers : BaseClass
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        [ForeignKey("AnswerSelfOption")]
        public Guid AnswerForself { get; set; }
        public QuestionOption AnswerSelfOption { get; set; }

        [ForeignKey("AnswerPartnerOption")]
        public Guid AnswerForPartner { get; set; }
        public QuestionOption AnswerPartnerOption { get; set; }

        public DateTime AnswerDate { get; set; }
    }
    //public class UserAnswers : BaseClass
    //{
    //    [ForeignKey("User")]
    //    public Guid UserId { get; set; }
    //    public User User { get; set; }
    //    [ForeignKey("Question")]
    //    public Guid QuestionId {  get; set; }
    //    public Question Question { get; set; }
    //    //option id question option sy
    //    [ForeignKey("QuestionOption")]
    //    public Guid AnswerForself { get; set; }
    //    public QuestionOption AnswerSelfOption { get; set; }
    //    [ForeignKey("QuestionOption")]
    //    public Guid AnswerForPartner { get; set; }
    //    public QuestionOption AnswerPartnerOption { get; set; }
    //    public DateTime AnswerDate { get; set; }

    //}
}
