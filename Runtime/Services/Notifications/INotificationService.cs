using System;
using System.Collections;
using TheForge.Services.Notifications.Dto;
using Unity.Notifications;

namespace TheForge.Services.Notifications
{
    public interface INotificationService : ISingleton
    {
        IEnumerator RequestPermission(Action<NotificationsPermissionStatus> onRequestReturned);
        void ScheduleNotification(NotificationRequestDto notificationRequest);
        void CancelAllNotifications();
        void OpenNotificationSettings();
    }
}