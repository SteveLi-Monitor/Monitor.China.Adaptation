using Domain.Extensions;
using Sap.Data.SQLAnywhere;
using System.Data;

namespace Monitor.China.Api.Bootstrap
{
    public class SaDbConnection
    {
        public SaDbConnection(ConnectionStringService connectionStringService)
        {
            ConnectionString = connectionStringService.GetAsync().GetAwaiter().GetResult();
            DbConnection = new SAConnection(ConnectionString);
        }

        public IDbConnection DbConnection { get; private set; }

        public string ConnectionString { get; private set; }

        public ConnectionState ConnectionState
        {
            get
            {
                DbConnection.Guard(nameof(IDbCommand));
                return DbConnection.State;
            }
        }

        public void Open()
        {
            if (ConnectionState != ConnectionState.Open)
            {
                DbConnection.Open();
            }
        }

        public void Close()
        {
            DbConnection?.Close();
        }
    }
}
