using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public string StaffNo { get; set; }
        public string StartDate { get; set; }

        public string CreatedBy { get; set; }
        public string EndDate { get; set; }

       public virtual HistoryModel Periods { get; set; }

        public HistoryModel()
        {
          
            new HistoryModel();
        }
        public HistoryModel(History histories)
        {
            this.Assign(histories);
          
            Periods = new HistoryModel();

        }
        public History Create(HistoryModel model)
        {
            return new History
            {
              
                PeriodId = model.PeriodId,
                  EndDate = model.EndDate,
                   StartDate = model.StartDate,
                   CreatedBy = model.CreatedBy
              
            };
        }
    }
}