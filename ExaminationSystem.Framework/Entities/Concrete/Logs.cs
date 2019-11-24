using System;

namespace ExaminationSystem.Framework.Entities.Concrete
{
    public class Log
    {
        public string Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string Audit { get; set; }
    }
}