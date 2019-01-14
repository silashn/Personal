using Pages.Settings.Database;
using Pages.Tools.Extensions;

namespace Pages.Data.Clients
{
    public class DatabaseClient
    {
        protected DatabaseSettings settings { get; }

        public DatabaseClient(DatabaseSettings settings)
        {
            settings.ThrowIfParameterIsNull(nameof(settings));
            this.settings = settings;
        }
    }
}
