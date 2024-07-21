﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetTaskListQuery: IRequest<List<GetTaskListViewModel>>
    {

    }
}
