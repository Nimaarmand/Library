﻿namespace Domain.Entities.Commons
{
    public abstract class BaseEntityNotId
    {
        public DateTime InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int IsDeleted { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string? InsertUserId { get; set; }
        public string? UpdateUserId { get; set; }
        public string? DeleteUserId { get; set; }
    }
}
