using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Payloads.SubTypes
{
    public class DateTimeTimeStemp
    {
        public long stemp {  get; set; }

        public DateTimeTimeStemp(long stemp)
        {
            this.stemp = stemp;
        }

        public static DateTimeTimeStemp FromDateTime(DateTime dateTime)
        {
            var stemp = dateTime - DateTime.UnixEpoch;
            
            return new DateTimeTimeStemp(stemp.Ticks);
        }

        public DateTime ToDateTime()
        {
            return DateTime.UnixEpoch.AddTicks(stemp);
        }
    }
}
