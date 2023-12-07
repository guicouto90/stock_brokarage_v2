namespace Application.Shared
{
    public class Output<T> where T : class, new()
    {
        public bool IsValid { get; private set; }

        public T? Data { get; private set; }

        public string? ErrorMessage { get; private set; }

        public Output() 
        {
            IsValid = false;
            Data = null;
        }

        public Output(bool isValid, T? data)
        {
            IsValid = isValid;
            Data = data;
        }

        public Output(T? data)
        {
            Data = data;
        }

        public void AddErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }

        public void AddResult(T? result)
        {
            Data = result;
            IsValid = true;
        }
    }
}
