﻿namespace Application.Dtos.Commons
{
    public class BaseDto
    {
        public string InsertTime { get; set; } = "";
        public string? UpdateTime { get; set; }
        public bool IsActive { get; set; } = true;
        //public string? InsertUser { get; set; } Name Family User
        //public string? UpdateUser { get; set; } Name Family User
    }
}
