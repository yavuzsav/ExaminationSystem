using ExaminationSystem.Framework.Utilities.Results.BaseResults;

namespace ExaminationSystem.Framework.Utilities.Results.ErrorResults
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}