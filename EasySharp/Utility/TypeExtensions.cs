namespace EasySharp.Utility;

public static class TypeExtensions
{
    public static bool IsListOf<T>(this Type type, out Type elementType)
    {
        elementType = null;
    
        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
        {
            return false;
        }
    
        elementType = type.GetGenericArguments()[0];
        return typeof(T).IsAssignableFrom(elementType);
    }
}