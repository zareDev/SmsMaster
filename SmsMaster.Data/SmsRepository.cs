using SmsMaster.Data.Interfaces;
using SmsMaster.Model;
using SmsMaster.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SmsMaster.Data
{
    public class SmsRepository : GenericRepository<Sms>, ISmsRepository
    {
        private static Dictionary<string, Expression<Func<Sms, object>>> smsColumnMap = new Dictionary<string, Expression<Func<Sms, object>>>
        {
            ["DateTime"] = e => e.DateTime
        };

        public SmsRepository(SmsMasterContext context):base(context)
        {
            columnsMap = smsColumnMap;
        }

        public async Task<QueryResult<Sms>> Get(SmsQuery smsQuery)
        {
            var result = new QueryResult<Sms>();
            var query = _context.Sms.Where(e => e.DateTime >= smsQuery.DateTimeFrom && e.DateTime <= smsQuery.DateTimeTo);
            result.TotalCount = await query.CountAsync();
            result.Items = await query.Skip(smsQuery.Skip).Take(smsQuery.Take).ToListAsync();
            return result;
        }

    }
}
