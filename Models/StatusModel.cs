using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentWork.Models
{
    public class StatusModel
    {
        public int nID { get; set; }
        public string sName { get; set; }

        public StatusModel(int id, string sn)
        {
            this.nID = id;
            this.sName = sn;
        }
    }
}