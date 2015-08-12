using System.ServiceModel;

namespace WcfServiceSumator
{
    [ServiceContract]
    public interface IServiceSumator
    {

        [OperationContract]
        long Sum(int a, int b);
    }
}
