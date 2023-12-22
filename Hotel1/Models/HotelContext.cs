using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ctpt> Ctpts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGHIA;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Ctpt>(entity =>
        {
            entity.HasKey(e => new { e.Mapt, e.Cmnd }).HasName("PK_CTPD");

            entity.ToTable("CTPT");

            entity.HasIndex(e => e.Cmnd, "IX_CTPD_MAPHONG");

            entity.Property(e => e.Mapt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MAPT");
            entity.Property(e => e.Cmnd)
                .HasMaxLength(50)
                .HasColumnName("CMND");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
