using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[PrimaryKey("Mahd", "Maphong")]
[Table("CTHOADON")]
public partial class Cthoadon
{
    [Key]
    [Column("MAHD")]
    [StringLength(10)]
    public string Mahd { get; set; } = null!;

    [Key]
    [Column("MAPHONG")]
    [StringLength(10)]
    public string Maphong { get; set; } = null!;

    [Column("SONGAY")]
    public int Songay { get; set; }

    [Column("DONGIA", TypeName = "money")]
    public decimal Dongia { get; set; }

    [Column("THANHTIEN", TypeName = "money")]
    public decimal Thanhtien { get; set; }

    [ForeignKey("Mahd")]
    [InverseProperty("Cthoadons")]
    public virtual Hoadon MahdNavigation { get; set; } = null!;
}
