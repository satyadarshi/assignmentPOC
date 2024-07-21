﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class CompleteTaskDetailCommand : IRequest<bool>
    {
        public int TaskDetailId { get; set; }
    }
}