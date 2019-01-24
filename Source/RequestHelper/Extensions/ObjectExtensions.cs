using RequestHelper.Enums;

namespace RequestHelper.Extensions
{
    public static class ObjectExtensions
    {
        public static TypeDefinition GetUrlType(this object dataObject)
        {
            return dataObject.GetType().GetUrlType();
        }
    }
}
