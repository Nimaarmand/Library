using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Commons.ErrorException
{
    public class ErrorMasterDto
    {
        public int StatusCode { get; set; }
        public ICollection<ErrorDetailsDto> ErrorDetails { get; set; } = new List<ErrorDetailsDto>();
    }
}
