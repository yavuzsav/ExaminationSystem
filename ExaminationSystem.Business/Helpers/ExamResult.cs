using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Business.Helpers
{
    public class ExamResult
    {
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Empty { get; set; }
    }
}