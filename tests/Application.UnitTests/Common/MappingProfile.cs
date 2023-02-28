using Common.Utils;
using System.Reflection;

namespace Application.UnitTests.Common
{
    public class MappingProfile : MappingProfileHelper
    {
        /// <summary>
        /// Constructor created for testing purpouses
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
