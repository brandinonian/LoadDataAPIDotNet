using System;
namespace LoadDataAPI.Models
{
    public class CustomLoadSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CustomCollectionName { get; set; } = null!;
    }
}

