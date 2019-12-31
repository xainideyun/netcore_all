using HT.Future.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HT.Future.Entities
{
    /// <summary>
    /// 用户地址表
    /// </summary>
    [Table("Address")]
    public class Address : BaseEntity
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Phone { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }

    /// <summary>
    /// Fluent Api
    /// </summary>
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(a => a.User).WithMany(a => a.Addresses).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }

}
