using AzmoonSaz.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.DTOs
{
    public record ResultDto
    {
        public ServiceStatus Status { get; set; }
        public string Message { get; set; }
    }
    public record ResultDto<T> : ResultDto
    {
        public T Data { get; set; }
    }
}
