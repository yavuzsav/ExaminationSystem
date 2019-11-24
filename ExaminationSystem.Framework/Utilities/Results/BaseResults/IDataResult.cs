namespace ExaminationSystem.Framework.Utilities.Results.BaseResults
{
    public interface IDataResult<out TData> : IResult
    {
        TData Data { get; }
    }
}