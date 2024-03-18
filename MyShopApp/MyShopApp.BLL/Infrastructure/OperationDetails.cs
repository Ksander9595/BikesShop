

namespace MyShopApp.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
        public OperationDetails(bool succeeded, string message, string prop)
        {
            Succeeded = succeeded;
            Message = message;  
            Property = prop;
        }
    }
}
