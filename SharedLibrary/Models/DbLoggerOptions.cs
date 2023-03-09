
namespace SharedLibrary.Models
{
    public class DbLoggerOptions
    {
        public string ConnectionString { get; init; }

        public string[] LogFields { get; init; } = new string[0];

        public string LogTable { get; init; }
        public string LogInfoTable { get; init; }

        public DbLoggerOptions()
        {
        }
    }
}
