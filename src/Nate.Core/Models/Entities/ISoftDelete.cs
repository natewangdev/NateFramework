﻿namespace Nate.Core.Models.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedTime { get; set; }
        string? DeletedBy { get; set; }
    }
}
