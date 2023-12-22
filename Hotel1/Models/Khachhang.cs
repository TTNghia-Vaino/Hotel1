using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel1.Models;

[Table("KHACHHANG")]
public partial class Khachhang
{
    [Key]
    [Column("CMND")]
    [StringLength(50)]
    public string Cmnd { get; set; } = null!;

    [Column("TEN")]
    [StringLength(50)]
    public string Ten { get; set; } = null!;

    [Column("NGAYSINH", TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Ngaysinh { get; set; }

    [Column("DIACHI")]
    public string Diachi { get; set; } = null!;

    [Column("PHAI")]
    public bool? Phai { get; set; } 

    [Column("SDT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Sdt { get; set; } = null;

    [Column("EMAIL")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; } = null;

    [Column("MALK")]
    [StringLength(20)]
    [Unicode(true)]
    public string Malk { get; set; } = null!;

    [ForeignKey("Cmnd")]
    [InverseProperty("Cmnds")]
    public virtual ICollection<Phieuthue> Mapts { get; } = new List<Phieuthue>();
}
