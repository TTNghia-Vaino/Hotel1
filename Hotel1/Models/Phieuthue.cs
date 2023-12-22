using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[Table("PHIEUTHUE")]
public partial class Phieuthue
{
    [Key]
    [Column("MAPT")]
    [StringLength(10)]
    public string Mapt { get; set; } = null!;

    [Column("MAPHONG")]
    [StringLength(10)]
    public string Maphong { get; set; } = null!;

    [Column("TRANGTHAI")]
    [StringLength(10)]
    public string Trangthai { get; set; } = null!;

    [Column("NGAYDAT", TypeName = "date")]
    public DateTime Ngaydat { get; set; }

    [Column("NGAYBDTHUE", TypeName = "date")]
    public DateTime Ngaybdthue { get; set; }

    [Column("NGAYKTTHUE", TypeName = "date")]
    public DateTime? Ngayktthue { get; set; }

    [InverseProperty("MaptNavigation")]
    public virtual ICollection<Hoadon> Hoadons { get; } = new List<Hoadon>();

    [ForeignKey("Maphong")]
    [InverseProperty("Phieuthues")]
    public virtual Phong MaphongNavigation { get; set; } = null!;

    [ForeignKey("Mapt")]
    [InverseProperty("Mapts")]
    public virtual ICollection<Khachhang> Cmnds { get; } = new List<Khachhang>();
}
