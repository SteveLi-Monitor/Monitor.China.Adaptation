namespace Application.Common.ErrorMessages
{
    public class EntityAlreadyExists : IErrorMessage
    {
        public EntityAlreadyExists(string entityName, string propertyName, object propertyValue)
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public string EntityName { get; private set; }

        public string PropertyName { get; private set; }

        public object PropertyValue { get; private set; }

        public string ToMessage()
        {
            return $"{EntityName} already exists. " +
                $"Property name: {PropertyName}. Property value: {PropertyValue}.";
        }
    }
}
