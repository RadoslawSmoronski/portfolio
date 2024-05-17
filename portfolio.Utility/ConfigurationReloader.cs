using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility
{
    public class ConfigurationReloader
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly string _filePath;

        public ConfigurationReloader(IConfigurationRoot configurationRoot, string filePath)
        {
            _configurationRoot = configurationRoot;
            _filePath = filePath;
            WatchConfigurationFile();
        }

        private void WatchConfigurationFile()
        {
            var watcher = new FileSystemWatcher(Path.GetDirectoryName(_filePath))
            {
                Filter = Path.GetFileName(_filePath),
                NotifyFilter = NotifyFilters.LastWrite
            };

            watcher.Changed += (sender, args) => _configurationRoot.Reload();
            watcher.EnableRaisingEvents = true;
        }
    }
}
