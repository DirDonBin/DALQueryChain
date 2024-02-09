// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Data;

#pragma warning disable 1573, 1591
#nullable enable

namespace ManualTest.Linq2Db.Context
{
	public partial class TestContext : DataConnection
	{
		public TestContext()
		{
			InitDataContext();
		}

		public TestContext(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		public TestContext(DataOptions<TestContext> options)
			: base(options.Options)
		{
			InitDataContext();
		}

		partial void InitDataContext();

		public ITable<ArchiveProduct> ArchiveProducts => this.GetTable<ArchiveProduct>();
		public ITable<Category>       Categories      => this.GetTable<Category>();
		public ITable<Product>        Products        => this.GetTable<Product>();
		public ITable<VersionInfo>    VersionInfos    => this.GetTable<VersionInfo>();
	}
}
