﻿namespace AOGSystem.Domain;
public class BaseEntity
{
    public int Id { get;  set; }
    public DateTime CreatedAT { get;  set; }
    public DateTime? UpdatedAT { get;  set;  }
    public string? CreatedBy { get;  set; }
    public string? UpdatedBy { get;set; }
}
