using FirstAPI.AppEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstAPI.DTO
{
    public class ItemsDTO
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public StatusEnum Status
        {
            get;
            set;
        }
        public int Hours
        {
            get;
            set;
        }
        public int Minute
        {
            get;
            set;
        }
    }
}