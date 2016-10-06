using FirstAPI.AppEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstAPI.Models
{
    public class Items
    {
        private int _id;
        private string _title;
        private string _description;
        private DateTime _date;
        private StatusEnum _status;
        private int _hours;
        private int _minute;

       
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
            
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public StatusEnum Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }
        public int Minute
        {
            get { return _minute; }
            set { _minute = value; }
        }
    }
}