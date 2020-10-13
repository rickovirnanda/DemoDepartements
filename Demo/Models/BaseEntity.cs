﻿using Demo.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class BaseEntity : IEntity
    {
        [Key]
        public long Id { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}