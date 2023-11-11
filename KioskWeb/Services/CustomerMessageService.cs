using Entities.Context;
using Entities.Models;
using Services.Base;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerMessageService : BaseService<CustomerMessage>, ICustomerMessageService
    {
        private ProjectContext _context;
        public CustomerMessageService(ProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
