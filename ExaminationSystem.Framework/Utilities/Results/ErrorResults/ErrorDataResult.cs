﻿using ExaminationSystem.Framework.Utilities.Results.BaseResults;

namespace ExaminationSystem.Framework.Utilities.Results.ErrorResults
{
    public class ErrorDataResult<TData> : DataResult<TData>
    {
        public ErrorDataResult(TData data, string message) : base(data, false, message)
        {
        }

        public ErrorDataResult(TData data) : base(data, false)
        {
        }

        public ErrorDataResult(string message) : base(default, false, message)
        {
        }

        public ErrorDataResult() : base(default, false)
        {
        }
    }
}