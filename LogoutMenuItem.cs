using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBTodo
{

    public class LogoutMenuItem
    {
        public LogoutMenuItem()
        {
            TargetType = typeof(LogoutDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}