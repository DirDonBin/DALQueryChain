// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using DALQueryChain.Interfaces;
using LinqToDB.Mapping;
using System;

#pragma warning disable 1573, 1591
#nullable enable

namespace ManualTest.Linq2Db.Context
{
	[Table("VersionInfo")]
	public class VersionInfo : IDbModelBase
	{
		[Column("Version"    )] public long      Version     { get; set; } // bigint
		[Column("AppliedOn"  )] public DateTime? AppliedOn   { get; set; } // timestamp (6) without time zone
		[Column("Description")] public string?   Description { get; set; } // character varying(1024)
	}
}
