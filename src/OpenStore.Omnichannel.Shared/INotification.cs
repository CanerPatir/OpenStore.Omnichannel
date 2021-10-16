// ReSharper disable CheckNamespace

using System;
using System.Globalization;

namespace OpenStore.Omnichannel;

public interface INotification
{
    Guid Id { get; }
    Guid UserId { get; }
    DateTime Time { get; }
    string Title { get; }
    string Message { get; }
    CultureInfo Culture { get; }
    NotificationChannel Channel { get; }
    DateTime CreatedAt { get; }
    bool IsDelivered { get; }
    bool? IsReceived { get; }
    string ActionLink { get; }
}