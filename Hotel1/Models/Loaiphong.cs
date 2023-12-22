using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[Table("LOAIPHONG")]
public partial class Loaiphong
{
    [Key]
    [Column("MALP")]
    [StringLength(10)]
    public string Malp { get; set; } = null!;

    [Column("SOLUONGTOIDA")]
    public int Soluongtoida { get; set; }

    [Column("GIALOAIPHONG", TypeName = "money")]
    public decimal Gialoaiphong { get; set; }

    [InverseProperty("LoaiphongNavigation")]
    public virtual ICollection<Phong> Phongs { get; } = new List<Phong>();
}
