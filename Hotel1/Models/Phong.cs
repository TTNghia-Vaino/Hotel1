using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[Table("PHONG")]
[Index("Loaiphong", Name = "IX_PHONG_HANGPHONG")]
public partial class Phong
{
    [Key]
    [Column("MAPHONG")]
    [StringLength(10)]
    public string Maphong { get; set; } = null!;

    [Column("LOAIPHONG")]
    [StringLength(10)]
    public string Loaiphong { get; set; } = null!;

    [Column("TRANGTHAI")]
    [StringLength(10)]
    public string Trangthai { get; set; } = null!;

    [Column("SOLUONGKHACH")]
    public int? Soluongkhach { get; set; }

    [ForeignKey("Loaiphong")]
    [InverseProperty("Phongs")]
    public virtual Loaiphong LoaiphongNavigation { get; set; } = null!;

    [InverseProperty("MaphongNavigation")]
    public virtual ICollection<Phieuthue> Phieuthues { get; } = new List<Phieuthue>();
}
