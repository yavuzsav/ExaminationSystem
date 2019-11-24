using ExaminationSystem.Framework.Utilities.Results.BaseResults;

namespace ExaminationSystem.Framework.Utilities.Results.SuccessResults
{
    public class SuccessDataResult<TData> : DataResult<TData>
    {
        public SuccessDataResult(TData data, string message) : base(data, true, message)
        {
        }

        public SuccessDataResult(TData data) : base(data, true)
        {
        }

        public SuccessDataResult(string message) : base(default, true, message)
        {
        }

        public SuccessDataResult() : base(default, true)
        {
        }
    }
}