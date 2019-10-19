namespace Xplorium.SqlServer
{
    using System.Data.SqlTypes;
    using Microsoft.SqlServer.Server;

    public class UDFs
    {
        [SqlFunction]
        public static SqlString Sample()
        {
            // Put your code here
            return new SqlString("Hello");
        }
    };
}

