using System.Runtime.Serialization;

namespace RestApi.Application.V1.Shared
{
    public class Result
    {
        private readonly List<string> _errors;

        protected Result()
        {
            _errors = new List<string>();
        }

        [IgnoreDataMember]
        public bool IsValid => _errors.Count == 0;
        public IEnumerable<string> Errors => _errors.AsEnumerable();

        public static Result Create()
        {
            return new Result();
        }

        public Result Error(string message)
        {
            AddError(message);
            return this;
        }

        public Result Error(IEnumerable<string> messages)
        {
            AddErrors(messages);
            return this;
        }

        public void AddErrors(IEnumerable<string> messages)
        {
            _errors.AddRange(messages);
        }
        
        public void AddError(string message)
        {
            _errors.Add(message);
        }
    }
}
