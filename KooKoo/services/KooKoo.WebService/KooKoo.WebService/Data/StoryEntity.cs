using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KooKoo.WebService.Data {

    public class StoryEntity : TableEntity {

        public Guid? Id { get; set; }
        public string StoryText { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
 
        public Guid? PublisherId { get; set; }

        public Guid? PlaceId { get; set; }
        public double? PlaceLantitude { get; set; }
        public double PlaceLongitude { get; set; }
        public string PlaceName { get; set; }
        public bool HasImage { get; set; }
        public DateTimeOffset CreationTime { get; set; }

        public Uri ImageUri {
            get {
                if (this.HasImage) {
                    return new Uri("http://kookoo.blob.core.windows.net/stories/" + this.Id.ToString() );
                }

                return null;
            }
        }

        public string TimeAgo {
            get {
                return this.GetFriendlyTimeStamp();
            }
        }

        private string GetFriendlyTimeStamp() {

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            TimeSpan ts = DateTime.UtcNow - this.CreationTime;
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 0) {
                return "not yet";
            }
            if (delta < 1 * MINUTE) {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE) {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE) {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE) {
                return "an hour ago";
            }
            if (delta < 24 * HOUR) {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * HOUR) {
                return "yesterday";
            }
            if (delta < 30 * DAY) {
                return ts.Days + " days ago";
            }
            if (delta < 12 * MONTH) {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            } else {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        //public override string RowKey {
        //    get {
        //        if (m_rowKey == null) {
        //            m_rowKey = Id.ToString();
        //        }
        //        return m_rowKey;
        //    }
        //    set {
        //        m_rowKey = value;
        //    }
        //}

        //public override string PartitionKey {
        //    get {
        //        if (m_partitionKey == null) {
        //            m_partitionKey =
        //                Math.Round(Longitude, 1).ToString(CultureInfo.InvariantCulture) +
        //                Math.Round(Latitude, 1).ToString(CultureInfo.InvariantCulture) +
        //                Date.Year.ToString(CultureInfo.InvariantCulture) +
        //                Date.Month.ToString(CultureInfo.InvariantCulture);
        //        }
        //        return m_partitionKey;
        //    }
        //    set {
        //        m_partitionKey = value;
        //    }
        //}
    }
}