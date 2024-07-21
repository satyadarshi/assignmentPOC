using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class  UpdateTaskDetailCommand : IRequest<bool>
    {
        public UpdateTaskDetailDto UpdateDetails { get; set; }
    }

}
