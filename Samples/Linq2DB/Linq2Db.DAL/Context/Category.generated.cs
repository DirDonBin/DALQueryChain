// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using DALQueryChain.Interfaces;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace ManualTest.Linq2Db.Context
{
	[Table("Categories")]
	public class Category : IDbModelBase
	{
		[Column("Id"     , IsPrimaryKey = true , IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int      Id      { get; set; } // integer
		[Column("Name"   , CanBeNull    = false                                                             )] public string   Name    { get; set; } = null!; // character varying(150)
		[Column("Created"                                                                                   )] public DateTime Created { get; set; } // date

		#region Associations
		/// <summary>
		/// Products_CategoryId_fkey backreference
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Product.CategoryId))]
		public List<Product> Products { get; set; } = null!;
		#endregion
	}
}