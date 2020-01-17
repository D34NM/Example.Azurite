using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.Core.Contracts;
using Example.Core.Models;
using Microsoft.Extensions.Logging;

namespace Example.Core.Azure
{
    internal sealed class StorageLoggerDecorator : IStorage
    {
        private readonly ILogger<StorageLoggerDecorator> _logger;
        private readonly IStorage _decorated;

        public StorageLoggerDecorator(ILogger<StorageLoggerDecorator> logger, IStorage decoreated)
        {
            _logger = logger;
            _decorated = decoreated;
        }

        public Task SaveAsync(File file, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting uplaod of the {file}");

            var task = _decorated.SaveAsync(file, cancellationToken);

            _logger.LogInformation($"Finished uplaod of the {file}");

            return task;
        }

        public Task SaveAsync(IReadOnlyCollection<File> files, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting uplaod of the {files}");

            var task = _decorated.SaveAsync(files, cancellationToken);

            _logger.LogInformation($"Finished uplaod of the {files}");

            return task;
        }
    }
}
