using Common.Errors;
using Microsoft.Extensions.Configuration;

namespace Application.UnitTests.Common
{
    public class ErrorDataFactory
    {
        public static ErrorData Create()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"Errors/Errors.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.Get<ErrorData>();
        }
    }
}
