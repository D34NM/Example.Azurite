using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Examples.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using File = Examples.Core.Models.File;

namespace Examples.Api.Controllers
{
    [ApiController]
    [Route("api/example")]
    public class ExampleController : ControllerBase
    {
        private const string TmpDirectory = "tmp";

        private readonly IStorage _storage;
        private readonly IFileSystem _fileSystem;

        public ExampleController(IStorage storage, IFileSystem fileSystem)
        {
            _storage = storage;
            _fileSystem = fileSystem;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
        {
            var mapped = await Map(Request.Form.Files, cancellationToken); // todo: parameter binding, had some issues with [FromForm]

            await _storage.SaveAsync(mapped, cancellationToken);

            return Ok();
        }

        private async Task<IReadOnlyCollection<File>> Map(IEnumerable<IFormFile> files, CancellationToken cancellationToken)
        {
            var mapped = new List<File>();

            var tmpDirectory = _fileSystem.Path.Combine(_fileSystem.Directory.GetCurrentDirectory(), TmpDirectory);

            _fileSystem.Directory.CreateDirectory(tmpDirectory);

            foreach (var file in files)
            {
                var filePath = _fileSystem.Path.Combine(tmpDirectory, file.FileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                mapped.Add(new File(filePath));
            }

            return mapped;
        }
    }
}