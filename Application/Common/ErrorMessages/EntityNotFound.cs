namespace Application.Common.ErrorMessages
{
    public class EntityNotFound : IErrorMessage
    {
        public EntityNotFound(string entityName, string propertyName, object propertyValue)
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public EntityNotFound(string entityName, object propertyValue)
            : this(entityName, "Id", propertyValue)
        {
        }

        public string EntityName { get; private set; }

        public string PropertyName { get; private set; }

        public object PropertyValue { get; private set; }

        public string ToMessage()
        {
            return $"{EntityName} not found. " +
                $"Property name: {PropertyName}. Property value: {PropertyValue}.";
        }
    }
}
