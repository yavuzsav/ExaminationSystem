using ExaminationSystem.Framework.Utilities.Results.BaseResults;

namespace ExaminationSystem.Framework.Utilities.Results.SuccessResults
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true)
        {
        }
    }
}