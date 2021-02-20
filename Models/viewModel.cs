using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentWork.Models
{
    public class viewModel
    {
        public List<CleintModel> clients { get; set; }
        public List<StateModel> states { get; set; }
        public List<StatusModel> statuses { get; set; }
    }
}