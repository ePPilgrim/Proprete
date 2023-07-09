using Proprette.Domain.Data.Common;

namespace Proprette.DataSeeding.DataSource.Models
{
    public class FileToWarehouse : IFileToModel
    {
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        public FileToWarehouse(string itemName)
        {
            ItemName = itemName;
        }

        public FileToWarehouse() : this(null!) { }
    }
}
