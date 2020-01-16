using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Examples.Core.Contracts;
using Examples.Core.Models;
using System.Linq;

namespace Examples.Core.Azure
{
    internal sealed class AzureBlobService : IStorage
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly IFileSystem _fileSystem;

        public AzureBlobService(BlobContainerClient blobContainerClient, IFileSystem fileSystem)
        {
            _blobContainerClient = blobContainerClient;
            _fileSystem = fileSystem;
        }

        public async Task SaveAsync(File file, CancellationToken cancellationToken)
        {
            var blob = _blobContainerClient.GetBlobClient(_fileSystem.Path.GetFileName(file));
            await blob.UploadAsync(file, cancellationToken);
        }

        public async Task SaveAsync(IReadOnlyCollection<File> files, CancellationToken cancellationToken)
        {
            var uploadTasks = files.Select(file => SaveAsync(file, cancellationToken));

            await Task.WhenAll(uploadTasks);
        }
    }
}
