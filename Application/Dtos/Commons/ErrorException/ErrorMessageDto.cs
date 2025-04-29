using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Commons.ErrorException
{

    public class ErrorMessageDto : ErrorDetailsDto
    {
        public int StatusCode { get; set; }

    }
}
