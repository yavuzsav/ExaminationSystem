namespace ExaminationSystem.Framework.Utilities.Results.BaseResults
{
    public class DataResult<TData> : Result, IDataResult<TData>
    {
        public DataResult(TData data, bool success, string message = "") : base(success, message)
        {
            Data = data;
        }

        public DataResult(TData data, bool success) : base(success)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}