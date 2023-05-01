using System.Runtime.Serialization;

namespace OrderServiceApp.DataContracts
{
    [DataContract]
    public class ExceptionDc
    {
        public ExceptionDc(string message)
        {
            this.Error = message;
        }

        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}