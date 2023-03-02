using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_document")]
public partial class MissionDocument
{
    [Key]
    [Column("mission_document_id")]
    public long MissionDocumentId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("document_name")]
    [StringLength(255)]
    public string? DocumentName { get; set; }

    [Column("document_type")]
    [StringLength(255)]
    public string? DocumentType { get; set; }

    [Column("document_path")]
    [StringLength(255)]
    public string? DocumentPath { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionDocuments")]
    public virtual Mission Mission { get; set; } = null!;
}
