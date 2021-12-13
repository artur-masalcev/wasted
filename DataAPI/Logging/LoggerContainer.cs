
using Microsoft.Extensions.Logging;

namespace DataAPI.Logging
{
    public class LoggerContainer
    {
        public ILogger Logger { get; set; }

        public LoggerContainer(ILogger logger)
        {
            Logger = logger;
        }
    }
}