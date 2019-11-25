namespace EMIS.PatientFlow.API
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }
        public int Outcome { get; set; }
        public string Error { get; set; }
    }
}
