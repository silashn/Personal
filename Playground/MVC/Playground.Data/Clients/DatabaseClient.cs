using Playground.Settings.Database;
using Playground.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Data.Clients
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
