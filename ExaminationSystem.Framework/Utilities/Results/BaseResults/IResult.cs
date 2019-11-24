namespace ExaminationSystem.Framework.Utilities.Results.BaseResults
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}