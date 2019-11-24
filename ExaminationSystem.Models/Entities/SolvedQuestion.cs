using ExaminationSystem.Models.Base;
using System;

namespace ExaminationSystem.Models.Entities
{
    public class SolvedQuestion : BaseEntityWithId
    {
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public string QuestionId { get; set; }
        public DateTime Date { get; set; }
    }
}