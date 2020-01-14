using System.ComponentModel.
namespace Examples.Core.Azure.Options
{
    public class BlobServiceOptions
    {
        [Required]
        public string ConnectionString { get; set; }
        
        [Required]
        public string Container { get; set; }
    }
}