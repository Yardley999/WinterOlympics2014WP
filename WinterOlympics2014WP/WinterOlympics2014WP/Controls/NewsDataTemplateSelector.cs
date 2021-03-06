﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Controls
{
    public class NewsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NewsTemplate { get; set; }

        public DataTemplate VideoTemplate { get; set; }

        public DataTemplate MoreButtonTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            News news = item as News;
            if (news != null)
            {
                if (news.IsMoreButton)
                {
                    return MoreButtonTemplate;
                }
                else
                {
                    switch (news.Type)
                    {
                        case "0"://video
                            return VideoTemplate;
                            break;
                        case "1"://news
                            return NewsTemplate;
                            break;
                        default:
                            break;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
