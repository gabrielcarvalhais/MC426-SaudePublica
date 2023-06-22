using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MC426_Backend.Utils
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var aux = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First();

            if (aux.CustomAttributes != null && aux.CustomAttributes.Count() > 0 && aux.GetCustomAttribute<DisplayAttribute>() != null)
            {
                return aux.GetCustomAttribute<DisplayAttribute>().GetName();
            }

            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .Name;
        }
    }
}
