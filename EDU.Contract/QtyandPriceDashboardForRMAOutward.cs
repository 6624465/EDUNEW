﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.EDU.Contract
{
   public class QtyandPriceDashboardForRMAOutward:IContract
    {
        public QtyandPriceDashboardForRMAOutward()
        {

        }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int MonthNo { get; set; }
        public string DocumentMonth { get; set; }
        public int DocumentYear { get; set; }
    }
}
