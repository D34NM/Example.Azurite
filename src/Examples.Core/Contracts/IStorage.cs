using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Examples.Core.Models;

namespace Examples.Core.Contracts
{
    public interface IStorage
    {
        Task SaveAsync(File file, CancellationToken cancellationToken);

        Task SaveAsync(IReadOnlyCollection<File> files, CancellationToken cancellationToken);
    }
}