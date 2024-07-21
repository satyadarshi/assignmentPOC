using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Common.Email
{
    public interface IEmailService
    {
        Task<bool> SendMail(string assignedTo, string taskName);
    }
}
