using System;
using System.Globalization;
using System.Text.Json.Serialization;
using OnlineCourse;

namespace OpenStore.Omnichannel.Shared.Dto
{
    public class NotificationDto : INotification
    {
        [JsonConstructor]
        public NotificationDto(Guid id, Guid userId, DateTime time, string title, string message, CultureInfo culture, NotificationChannel channel, DateTime createdAt, bool isDelivered,
            bool? isReceived, string actionLink)
        {
            Id = id;
            UserId = userId;
            Time = time;
            Title = title;
            Message = message;
            Culture = culture;
            Channel = channel;
            CreatedAt = createdAt;
            IsDelivered = isDelivered;
            IsReceived = isReceived;
            ActionLink = actionLink;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTime Time { get; }
        public string Title { get; }
        public string Message { get; }

        [JsonConverter(typeof(CultureConverter))]
        public CultureInfo Culture { get; }

        public NotificationChannel Channel { get; }

        public DateTime CreatedAt { get; }
        public bool IsDelivered { get; }
        public bool? IsReceived { get; set; }
        
        public string ActionLink { get; }

        #region equality

        protected bool Equals(NotificationDto other) => Id.Equals(other.Id);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NotificationDto) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        #endregion
       
    }
}