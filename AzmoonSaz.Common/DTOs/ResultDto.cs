using AzmoonSaz.Common.Enums;
using AzmoonSaz.Common.Utilities;
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

        private string _message;
        public string Message 
        {
            get
            {
                if (_message==null)
                {
                    return Status.ToDisplay();
                }

                return _message;
            }
            set
            {
                _message = value;
            }
        }
    }
    public record ResultDto<T> : ResultDto
    {
        public T Data { get; set; }
    }
}
