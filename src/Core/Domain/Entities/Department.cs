﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department : BaseEntity<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Description { get; set; }
    }
}
