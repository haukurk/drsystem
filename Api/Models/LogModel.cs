using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Api.Models
{
    public class LogModel
    {
        public LogModel()
        {
            Created = DateTime.Now;
            User = new UserBaseModel();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public int? Severity { get; set; }
        public DateTime Created { get; set; }
        public UserBaseModel User { get; set; }

        public string Url { get; set; }
    }
}