using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AES_test
{
    [ServiceContract]
    public interface IServiceAES
    {
        [OperationContract]
        string GetDecryption(string inputMessage, string iv, string pass);

        [OperationContract]
        Dictionary<string, string> GetEncryption(string inputMessage, string pass);
    }

}
