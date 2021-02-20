using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentWork.Models
{
    public class StateModel
    {
        public int nID { get; set; }
        public string sCode { get; set; }
        public string sName { get; set; }

        public StateModel(int id, string sc, string sn)
        {
            this.nID = id;
            this.sCode = sc;
            this.sName = sn;
        }
    }
}