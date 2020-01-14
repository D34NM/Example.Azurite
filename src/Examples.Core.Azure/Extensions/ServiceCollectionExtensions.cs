using System.IO.Abstractions;
using Azure.Storage.Blobs;
using Examples.Core.Azure.Options;
using Examples.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Examples.Core.Azure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlobServiceClient(this IServiceCollection services, BlobServiceOptions options)
        {
            services.AddSingleton<IStorage>(provider =>
            {
                var blobContainerClient = new BlobContainerClient(options.ConnectionString, options.Container);
                var fileSystem = provider.GetService<IFileSystem>();
                var logger = provider.GetService<ILogger<StorageLoggerDecorator>>();
                var decorated = new AzureBlobService(blobContainerClient, fileSystem);

                return new StorageLoggerDecorator(logger, decorated);
            });
            
            return services;
        }
    }
}