using System.Collections.Generic;

namespace Walmart {
   public struct SearchRequest {
      public string LsPublisherId;
      public string Keywords;
      public CategoryId? CategoryId;
      public int? Start;
      public Sort? Sort;
      public Order? Order;
      public ResponseGroup? ResponseGroup;
   }

   public struct SearchResponse {
      public string Query;
      public string Sort;
      public string Format;
      public string ResponseGroup;
      public int TotalResults;
      public int Start;
      public int NumItems;
      public IList<ResponseItem> ResponseItems;
   }

   public struct ResponseItem {
      public int ItemId;
      public int ParentItemId;
      public string Name;
      public double SalePrice;
      public string CategoryPath;
      public string ShortDescription;
      public string LongDescription;
      public string ThumbnailImageUrl;
      public string ProductTrackingUrl;
      public double StandardShipRate;
      public bool MarketPlace;
      public string ProductUrl;
      public string CategoryNode;
      public bool Bundle;
      public bool AvailableOnline;
   }
}
