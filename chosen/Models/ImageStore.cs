using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class ImageStore
    {
        public int ImageId { get; set; }
        public string Series { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ImageName { get; set; } = null!;
    }
}
