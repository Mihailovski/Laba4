using Laba4.Models;
using System;
using System.Collections.Generic;

namespace Laba4.ViewsModels
{
    public class DepartmentView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsReleasing { get; set; }
        public string Faculty { get; set; }
    }
}
