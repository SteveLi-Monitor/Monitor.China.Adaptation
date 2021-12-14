using System;

namespace Console.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName, string propertyName, object propertyValue)
            : base($"{entityName} already exists. Property name: {propertyName}. Property value: {propertyValue}.")
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public EntityAlreadyExistsException(string entityName, string propertyName, object propertyValue, Exception innerException)
            : base($"{entityName} already exists. Property name: {propertyName}. Property value: {propertyValue}.", innerException)
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public string EntityName { get; }

        public string PropertyName { get; }

        public object PropertyValue { get; }
    }
}
