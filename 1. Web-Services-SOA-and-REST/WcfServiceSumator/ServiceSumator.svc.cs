using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceSumator
{
    public class ServiceSumator : IServiceSumator
    {
        public long Sum(int a, int b)
        {
            return a + b;
        }
    }
}
