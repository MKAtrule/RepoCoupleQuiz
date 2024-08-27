//using System.ComponentModel.DataAnnotations.Schema;

//namespace Couple_Quiz.Models
//{
//    public class Progress : BaseClass
//    {
//        [ForeignKey("User")]
//        public Guid UserId { get; set; }
//        public User User { get; set; }

//        [ForeignKey("SessionHistory")]
//        public Guid SessionId { get; set; }
//        public SessionHistory SessionHistory { get; set; }

//        public int TotalQuestions { get; set; }
//        public int AnsweredQuestions { get; set; }
//        public int PendingQuestions { get; set; }

//        //public double ProgressPercentage
//        //{
//        //    get
//        //    {
//        //        return (double)AnsweredQuestions / TotalQuestions * 100;
//        //    }
//        //}
//    }
//}
