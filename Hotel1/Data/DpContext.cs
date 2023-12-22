using System;
using System.Collections.Generic;
using Hotel1.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Data;

public partial class DpContext : DbContext
{
    public DpContext()
    {
    }

    public DpContext(DbContextOptions<DpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cthoadon> Cthoadons { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaiphong> Loaiphongs { get; set; }

    public virtual DbSet<Phieuthue> Phieuthues { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGHIA;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Cthoadon>(entity =>
        {
            entity.Property(e => e.Mahd).IsFixedLength();
            entity.Property(e => e.Maphong).IsFixedLength();

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Cthoadons).HasConstraintName("FK_CTHOADON_HOADON");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.Property(e => e.Mahd).IsFixedLength();
            entity.Property(e => e.Mapt).IsFixedLength();

            entity.HasOne(d => d.MaptNavigation).WithMany(p => p.Hoadons).HasConstraintName("FK_HOADON_PHIEUDAT");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.Property(e => e.Malk).IsFixedLength();
        });

        modelBuilder.Entity<Loaiphong>(entity =>
        {
            entity.HasKey(e => e.Malp).HasName("PK_HANGPHONG");

            entity.Property(e => e.Malp).IsFixedLength();
        });

        modelBuilder.Entity<Phieuthue>(entity =>
        {
            entity.HasKey(e => e.Mapt).HasName("PK_PHIEUDAT_1");

            entity.Property(e => e.Mapt).IsFixedLength();
            entity.Property(e => e.Maphong).IsFixedLength();
            entity.Property(e => e.Trangthai).IsFixedLength();

            entity.HasOne(d => d.MaphongNavigation).WithMany(p => p.Phieuthues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUTHUE_PHONG");

            entity.HasMany(d => d.Cmnds).WithMany(p => p.Mapts)
                .UsingEntity<Dictionary<string, object>>(
                    "Ctpt",
                    r => r.HasOne<Khachhang>().WithMany()
                        .HasForeignKey("Cmnd")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CTPT_KHACHHANG"),
                    l => l.HasOne<Phieuthue>().WithMany()
                        .HasForeignKey("Mapt")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CTPT_PHIEUTHUE"),
                    j =>
                    {
                        j.HasKey("Mapt", "Cmnd").HasName("PK_CTPD");
                        j.ToTable("CTPT");
                        j.HasIndex(new[] { "Cmnd" }, "IX_CTPD_MAPHONG");
                    });
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.Property(e => e.Maphong).IsFixedLength();
            entity.Property(e => e.Loaiphong).IsFixedLength();
            entity.Property(e => e.Trangthai).IsFixedLength();

            entity.HasOne(d => d.LoaiphongNavigation).WithMany(p => p.Phongs).HasConstraintName("FK_PHONG_HANGPHONG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
