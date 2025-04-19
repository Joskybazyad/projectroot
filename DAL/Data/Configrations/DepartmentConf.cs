using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configrations
{
    internal class DepartmentConf : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.ID).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValue("GETDATE()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");




        }
    }
}
