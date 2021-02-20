using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentWork.Models
{
    public class CleintModel
    {
        public int nID { get; set; }
        public string sName { get; set; }
        public string sPhone { get; set; }
        public CleintModel(int id, string sn, string sp)
        {
            this.nID = id;
            this.sName = sn;
            this.sPhone = sp;
        }
    }
}