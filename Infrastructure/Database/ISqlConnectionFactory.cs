using Microsoft.Data.SqlClient;

namespace Infrastructure.Database
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
