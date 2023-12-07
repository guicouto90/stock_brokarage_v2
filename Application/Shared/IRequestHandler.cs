namespace Application.Shared
{
    public interface IRequestHandler<in TInput, TOutput>
    {
        Task<TOutput> ExecuteAsync(TInput? input);
    }

    public interface IRequestHandler<in TInput>
    {
        Task ExecuteAsync(TInput input);
    }

    public interface IRequestHandler
    {
        Task<object> ExecuteAsync();
    }
}
