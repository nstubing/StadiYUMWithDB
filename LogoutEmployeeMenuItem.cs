using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBTodo
{

    public class LogoutEmployeeMenuItem
    {
        public LogoutEmployeeMenuItem()
        {
            TargetType = typeof(LogoutEmployeeDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}