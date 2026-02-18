using System;
using System.Collections;
using TheForge.Services.Notifications.Dto;
using Unity.Notifications;

namespace TheForge.Services.Notifications
{
    /// <inheritdoc cref="NotificationService"/>
    public interface INotificationService : ISingleton
    {
        /// <inheritdoc cref="NotificationService.RequestPermission"/>
        IEnumerator RequestPermission(Action<NotificationsPermissionStatus> onRequestReturned);
        
        /// <inheritdoc cref="NotificationService.ScheduleNotification"/>
        void ScheduleNotification(NotificationRequestDto notificationRequest);
        
        /// <inheritdoc cref="NotificationService.CancelAllNotifications"/>
        void CancelAllNotifications();
        
        /// <inheritdoc cref="NotificationService.OpenNotificationSettings"/>
        void OpenNotificationSettings();
    }
}