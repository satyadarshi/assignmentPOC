using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using MediatR;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class CreateTaskDetailCommand : IRequest<bool>
    {
        public List<CreateTaskDetailDto> Details { get; set; }
    }

}
