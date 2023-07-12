using System;
namespace LoadDataAPI.Models
{
	public class FactoryLoadSettings
	{
		public string ConnectionString { get; set; } = null!;

		public string DatabaseName { get; set; } = null!;

		public string FactoryCollectionName { get; set; } = null!;
	}
}

