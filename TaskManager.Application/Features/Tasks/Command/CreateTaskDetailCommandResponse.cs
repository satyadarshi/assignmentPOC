using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class CreateTaskDetailCommandResponse : BaseResponse<string>
    {
        public CreateTaskDetailCommandResponse() : base()
        {

        }

        public CreateTaskDetailDto TaskDetail { get; set; }
    }
}
