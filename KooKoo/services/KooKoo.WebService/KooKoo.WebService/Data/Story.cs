using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KooKoo.WebService.Data {

    public class Story : TableEntity {

        private readonly Guid m_id;

        public Guid Id { get { return m_id;  } }
        public string StoryText { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
 
        public Guid? PublisherId { get; set; }

        public Guid? PlaceId { get; set; }
        public double? PlaceLantitude { get; set; }
        public double PlaceLongitude { get; set; }
        public string PlaceName { get; set; }

        public Story() {

            this.m_id = Guid.NewGuid();
            this.PartitionKey = "1";
            this.RowKey = this.Id.ToString();
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