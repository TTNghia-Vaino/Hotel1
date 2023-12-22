using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[Table("HOADON")]
[Index("Mapt", Name = "IX_HOADON_MAPD")]
public partial class Hoadon
{
    [Key]
    [Column("MAHD")]
    [StringLength(10)]
    public string Mahd { get; set; } = null!;

    [Column("MAPT")]
    [StringLength(10)]
    public string Mapt { get; set; } = null!;

    [Column("NGAYLAP", TypeName = "datetime")]
    public DateTime Ngaylap { get; set; }

    [InverseProperty("MahdNavigation")]
    public virtual ICollection<Cthoadon> Cthoadons { get; } = new List<Cthoadon>();

    [ForeignKey("Mapt")]
    [InverseProperty("Hoadons")]
    public virtual Phieuthue MaptNavigation { get; set; } = null!;
}
