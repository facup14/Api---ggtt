using System.Text.Json;

namespace Services.Common.Mapping
{
    public static class DTOMapperExtension
    {

        public static T MapTo<T>(this object value)
        {
            return JsonSerializer.Deserialize<T>(
                JsonSerializer.Serialize(value));
        }
    }
}
