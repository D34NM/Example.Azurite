using System;
using System.IO;
using System.IO.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Examples.Azurite.Tests
{
    public class AzuriteTests
    {
        private readonly IFileSystem _fileSystem;

        public AzuriteTests()
        {
            _fileSystem = new FileSystem();
        }

        [Fact]
        public async Task Run()
        {
            var fileContent = await _fileSystem.File.ReadAllBytesAsync(_fileSystem.Path.Combine("Resources", "test_file_one.txt"));

            using var client = new HttpClient();
            var content = CreateForm(fileContent);
            await client.PostAsync(new Uri("http://localhost:5000/api/example"), content);
        }

        private MultipartFormDataContent CreateForm(byte[] fileContent)
        {
            var memory = new MemoryStream(fileContent);
            var streamContent = new StreamContent(memory);
            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "test_file_one",
                FileName = "test_file_one.txt"
            };

            return new MultipartFormDataContent
            {
                { streamContent,"test_file_one.txt" }
            };
        }
    }
}
