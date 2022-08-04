// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using DALQueryChain.Interfaces;
using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Sample.Linq2Db.Context
{
	public static partial class IdentitySchema
	{
		public partial class DataContext
		{
			private readonly IDataContext _dataContext;

			public ITable<Role> Roles => _dataContext.GetTable<Role>();
			public ITable<User> Users => _dataContext.GetTable<User>();

			public DataContext(IDataContext dataContext)
			{
				_dataContext = dataContext;
			}
		}

		[Table("Roles", Schema = "identity")]
		public class Role : IDbModelBase
		{
			[Column("Id"  , IsPrimaryKey = true )] public Guid   Id   { get; set; } // uuid
			[Column("Name", CanBeNull    = false)] public string Name { get; set; } = null!; // character varying(150)

			#region Associations
			/// <summary>
			/// FK_Users_RoleId_Roles_Id backreference
			/// </summary>
			[Association(ThisKey = nameof(Id), OtherKey = nameof(User.RoleId))]
			public List<User> UsersRoleIdIds { get; set; } = null!;
			#endregion
		}

		[Table("Users", Schema = "identity")]
		public class User : IDbModelBase
		{
			[Column("Id"               , IsPrimaryKey = true )] public Guid      Id                { get; set; } // uuid
			[Column("RoleId"                                 )] public Guid?     RoleId            { get; set; } // uuid
			[Column("AccessFailedCount"                      )] public int       AccessFailedCount { get; set; } // integer
			[Column("Email"                                  )] public string?   Email             { get; set; } // character varying(350)
			[Column("EmailConfirmed"                         )] public bool      EmailConfirmed    { get; set; } // boolean
			[Column("PasswordHash"     , CanBeNull    = false)] public string    PasswordHash      { get; set; } = null!; // text
			[Column("UserName"         , CanBeNull    = false)] public string    UserName          { get; set; } = null!; // text
			[Column("Phone"                                  )] public string?   Phone             { get; set; } // character varying(30)
			[Column("PhoneConfirmed"                         )] public bool      PhoneConfirmed    { get; set; } // boolean
			[Column("TwoFactorEnabled"                       )] public bool      TwoFactorEnabled  { get; set; } // boolean
			[Column("CreateAt"                               )] public DateTime  CreateAt          { get; set; } // timestamp (6) without time zone
			[Column("ModifyAt"                               )] public DateTime  ModifyAt          { get; set; } // timestamp (6) without time zone
			[Column("DeleteAt"                               )] public DateTime? DeleteAt          { get; set; } // timestamp (6) without time zone

			#region Associations
			/// <summary>
			/// FK_Users_RoleId_Roles_Id
			/// </summary>
			[Association(ThisKey = nameof(RoleId), OtherKey = nameof(IdentitySchema.Role.Id))]
			public Role? Role { get; set; }
			#endregion
		}
	}
}
