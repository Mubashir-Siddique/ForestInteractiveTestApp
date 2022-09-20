using ForestInteractiveTestApp.Common;
using ForestInteractiveTestApp.IRepository;

namespace ForestInteractiveTestApp.Repository
{
    public class ConnectionFactory : IConnection
    {
        // Connection String Method to get ConnectionString from Config File
        public ConnectionFactory()
        {

        }
        public string ConnectionString
        {
            get
            {
                return APIConfig.Configuration?.GetSection("ConnectionStrings")["DefaultConnection"];
            }
        }
    }
}
