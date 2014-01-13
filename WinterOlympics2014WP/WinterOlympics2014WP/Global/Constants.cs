using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterOlympics2014WP
{
    public class Constants
    {
        public const string DOMAIN = "http://115.28.21.97";

        //splash
        public const string SPLASH_MODULE = "splash";
        public const string SPLASH_FILE_NAME = "splash.jpg";

        //news 
        public const string NEWS_MODULE = "news";
        public const string NEWS_FILE_NAME = "news.txt";

        //medal tally
        public const string MEDAL_TALLY_MODULE = "medaltally";
        public const string MEDAL_TALLY_FILE_NAME = "medaltally.txt";

        //category 
        public const string CATEGORY_MODULE = "category";
        public const string CATEGORY_LIST_FILE_NAME = "category_list.txt";

        //schedule
        public const string SCHEDULE_MODULE = "schedule";
        public const string SCHEDULE_FILE_NAME_FORMAT = "schedule_list_{0}.txt";
        public const string RESULT_FILE_NAME_FORMAT = "result_list_{0}.txt";


    }

    public class NaviParam
    {
        public const string CATEGORY_ID = "category_id";
    }
}
