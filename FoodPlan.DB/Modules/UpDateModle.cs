using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodPlan.DB.Modules
{
    public class UpDateModle
    {
        public string Set { get; set; }

        public string Inc { get; set; }     
        //private string _set;
        //private string _inc;

        //public string Set
        //{
        //    get => _set;
        //    set => _set = JsonConvert.SerializeObject(value);
        //}
        //public string Inc
        //{
        //    get => _inc;
        //    set => _inc = JsonConvert.SerializeObject(value);
        //}  
    }
}
