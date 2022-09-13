using System.Runtime.Serialization;

namespace RestApi.Application.V1.Shared
{
    public interface IResult
    {
        ICollection<string> Errors { get; }
        object? Value { get; }
        bool IsValid { get; }
        IResult Ok();
        IResult Error(string message);
        IResult Error(ICollection<string> messages);
    }
    
    public interface IResult<TValue> : IResult 
    {
        IResult<TValue> Error<T>(string message) where T : TValue;
        IResult<TValue> Error<T>(ICollection<string> messages) where T : TValue;
        IResult<TValue> Ok(TValue value);
    }

    public abstract class AbstractResult : IResult
    {
        public ICollection<string> Errors { get; protected set; }
        public object? Value { get; protected set; }
        protected bool Done { get; set; }

        [IgnoreDataMember]
        public bool IsValid => Errors.Count == 0;

        protected AbstractResult()
        {
            Done = false;
            Errors = new List<string>();
            Value = null;
        }

        public virtual IResult Ok()
        {
            Done = true;
            return this;
        }

        public virtual IResult Error(string message)
        {
            Done = true;
            Errors.Add(message);
            return this;
        }

        public virtual IResult Error(ICollection<string> messages)
        {
            Done = true;
            Errors = messages;
            return this;
        }
    }

    public class Result<TValue> : AbstractResult, IResult<TValue>
    {
        public IResult<TValue> Error<T>(string message) where T : TValue
        {
            Error(message);
            return this;
        }

        public IResult<TValue> Error<T>(ICollection<string> messages) where T : TValue
        {
            Error(messages);
            return this;
        }

        public IResult<TValue> Ok(TValue value)
        {
            Ok(value);
            return this;
        }
    }

    public class Result : AbstractResult
    {
        public static IResult Create()
        {
            return new Result();
        }

        public static IResult<T> Create<T>()
        {
            return new Result<T>();
        }
    }
}
