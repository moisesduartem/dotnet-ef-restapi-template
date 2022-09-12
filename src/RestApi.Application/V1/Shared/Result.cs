using System.Runtime.Serialization;

namespace RestApi.Application.V1.Shared
{
    public class Result<T> where T : class
    {
        [IgnoreDataMember]
        public bool Done { get; private set; }
        public Dictionary<string, IList<string>> Errors { get; private set; }
        public T? Value { get; private set; }

        private Result()
        {
            Done = false;
            Errors = new Dictionary<string, IList<string>> { };
            Value = null;
        }

        public static Result<T> Create()
        {
            return new Result<T>();
        }

        public Result<T> Ok()
        {
            Done = true;
            return this;
        }
        
        public Result<T> Ok(T value)
        {
            Done = true;
            Value = value;
            return this;
        }

        public Result<T> Error(string code, string message)
        {
            if (Errors.ContainsKey(code))
            {
                Errors[code].Add(message);
            } else
            {
                Errors.Add(code, new List<string> { message });
            }

            Done = true;
            
            return this;
        }

        public Result<T> Error(Dictionary<string, IList<string>> errors)
        {
            Errors = errors;
            Done = false;
            return this;
        }

        [IgnoreDataMember]
        public bool IsValid => Done && Errors.Count == 0;
    }
}
