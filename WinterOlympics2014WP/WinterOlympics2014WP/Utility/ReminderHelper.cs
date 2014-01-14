using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinterOlympics2014WP.Utility
{
    public class ReminderHelper
    {
        public static void AddReminder(string name, string title, string content, DateTime beginTime, string navUri)
        {
            Reminder reminder = new Reminder(name);
            reminder.Title = title;
            reminder.Content = content;
            reminder.BeginTime = beginTime;
            //reminder.ExpirationTime = expirationTime;
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.NavigationUri = new Uri(navUri, UriKind.Relative); ;

            try
            {
                var existingReminder = ScheduledActionService.Find(name);
                if (existingReminder != null)
                {
                    ScheduledActionService.Remove(name);
                }
                ScheduledActionService.Add(reminder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Register the reminder with the system.
        }

        public static void RemoveReminder(string name)
        {
            var reminder = ScheduledActionService.Find(name);
            if (reminder != null)
            {
                ScheduledActionService.Remove(name);
            }
        }

        public static ScheduledNotification GetReminder(string name)
        {
            var notifications = ScheduledActionService.GetActions<ScheduledNotification>();
            return notifications.FirstOrDefault(x => x.Name == name);
        }

        public static IEnumerable<ScheduledNotification> GetReminders()
        {
            var notifications = ScheduledActionService.GetActions<ScheduledNotification>();
            return notifications;
        }

        public static void ClearReminders()
        {
            var notifications = GetReminders();
            foreach (var item in notifications)
            {
                ScheduledActionService.Remove(item.Name);
            }
        }
    }
}
