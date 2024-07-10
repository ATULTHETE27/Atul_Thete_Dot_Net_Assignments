namespace EmployeeManagementSystem25.Common
{
    public class Credentials
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string EmployeeDataType = "Employee";
        public static readonly string ManagerDatatype = "Manager";
        public static readonly string AdditionalEmployee = "AdditionalEmployee";
        internal static readonly string VisitorUrl = Environment.GetEnvironmentVariable("visitorUrl");
        internal static readonly string AddVisitorEndPoint = "/api/Visitor/AddVisitor";
        internal static readonly string GetAllVisitorEndPoint = "api/Visitor/GetAllVisitors";
    }
}
