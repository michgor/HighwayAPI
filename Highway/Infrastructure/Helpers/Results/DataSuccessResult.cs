using System;
using System.Collections.Generic;
using System.Text;

namespace Highway.DAL.ResultObjects
{
    public class DataSuccessResult<T> : IDataResult<T>
    {
        public DataSuccessResult(T data)
        {
            Data = data;
        }

        public bool IsSuccess => true;

        public T Data { get; private set; }
    }
}
