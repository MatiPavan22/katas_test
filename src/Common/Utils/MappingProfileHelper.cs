using AutoMapper;
using Common.Interfaces;
using System.Reflection;

namespace Common.Utils
{
    public class MappingProfileHelper : Profile
    {
        public void ApplyMappingsFromAssembly(Assembly assembly)
        {
            foreach(Type item in (from t in assembly.GetExportedTypes()
                                  where t.GetInterfaces().Any((Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                                  select t).ToList())
            {
                object obj = Activator.CreateInstance(item);
                item.GetMethod("Mapping")?.Invoke(obj, new object[1] { this});
            }
        }
    }
}
