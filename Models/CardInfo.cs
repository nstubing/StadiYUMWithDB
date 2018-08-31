using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class CardInfo
    {
        public string CardNumber { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpyear { get; set; }
        public string CardCVV { get; set; }
    }
}
