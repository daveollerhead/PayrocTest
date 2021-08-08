namespace PayrocTest.Errors
{
    public class GenericErrors
    {
        public static string ParameterIsNull(string paramName)
        {
            return $"{paramName} cannot be null";
        }

        public static string EntityNotFound(string entityName)
        {
            return $"No {entityName} found with ID";
        }
    }
}