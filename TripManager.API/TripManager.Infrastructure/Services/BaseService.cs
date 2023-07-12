using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripManager.Domain;

namespace TripManager.Infrastructure.Services
{
    public class BaseService
    {
        internal readonly IMapper _mapper;
        internal readonly SQLContext _contextSql;

        public BaseService(IMapper mapper, SQLContext contextSql)
        {
            _mapper = mapper;
            _contextSql = contextSql;
        }
    }
}
