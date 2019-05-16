using Highway.DAL.ResultObjects;

namespace Highway.DAL.Helpers.Results
{
    public class DataFailedResult<T> : IDataResult<T>
    {
        public DataFailedResult()
        {
        }

        public bool IsSuccess => false;

        public T Data { get; }
    }    
}
