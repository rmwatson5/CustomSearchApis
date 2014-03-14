using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Walmart {
   public class WalmartSearchClient {
      #region Fields
      private const string BaseUrl = "http://walmartlabs.api.mashery.com/v1/";
      private readonly string _apiKey;
      #endregion
      #region Constructor
      public WalmartSearchClient(string apiKey) {
         if (String.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("apiKey is required");
         _apiKey = apiKey;
      }
      #endregion
      #region Api Calls
      public SearchResponse ItemSearch(SearchRequest request) {
         var response = new SearchResponse();

         var urlBuilder = new StringBuilder(String.Format("{0}search?apiKey={1}", BaseUrl, _apiKey));
         if (String.IsNullOrWhiteSpace(request.Keywords))
            throw new InvalidOperationException("Keyword is required");
         urlBuilder.Append(String.Format("&query={0}", request.Keywords));
         if (!String.IsNullOrWhiteSpace(request.LsPublisherId))
            urlBuilder.Append(String.Format("&lsPublisherId={0}", request.LsPublisherId));
         if (request.CategoryId != null)
            urlBuilder.Append(String.Format("&categoryId={0}", request.CategoryId.Value));
         if (request.Start != null)
            urlBuilder.Append(String.Format("&start={0}", request.Start));
         if (request.Sort != null) {
            urlBuilder.Append(String.Format("&sort={0}", Enum.GetName(typeof(Sort), request.Sort.Value)));
            if (request.Order != null)
               urlBuilder.Append(String.Format("&order={0}", Enum.GetName(typeof(Order), request.Order.Value)));
         }
         urlBuilder.Append("&format=xml");
         if (request.ResponseGroup != null)
            urlBuilder.Append(String.Format("&responseGroup={0}", Enum.GetName(typeof(ResponseGroup), request.ResponseGroup.Value)));

         var serverResponse = XDocument.Load(urlBuilder.ToString());

         response.Query = (from result in serverResponse.Descendants("query")
                           select result.Value).FirstOrDefault();

         response.Sort = (from result in serverResponse.Descendants("sort")
                          select result.Value).FirstOrDefault();

         response.Format = (from result in serverResponse.Descendants("format")
                            select result.Value).FirstOrDefault();

         response.ResponseGroup = (from result in serverResponse.Descendants("responseGroup")
                                   select result.Value).FirstOrDefault();

         var totalResultsValue = (from result in serverResponse.Descendants("totalResults")
                                  select result.Value).FirstOrDefault();
         Int32.TryParse(totalResultsValue, out response.TotalResults);

         var startValue = (from result in serverResponse.Descendants("start")
                           select result.Value).FirstOrDefault();
         Int32.TryParse(startValue, out response.Start);

         var numItemsValue = (from result in serverResponse.Descendants("numItems")
                              select result.Value).FirstOrDefault();
         Int32.TryParse(numItemsValue, out response.NumItems);

         var searchResultItems = from result in serverResponse.Descendants("item")
                                 select result;

         response.ResponseItems = CreateResponseItems(searchResultItems);

         return response;
      }

      public LookupResponse ItemLookup(LookupRequest request) {
         var response = new LookupResponse();

         var urlBuilder = new StringBuilder(String.Format("{0}items/{1}?apiKey={2}", BaseUrl, request.ItemId, _apiKey));
         if (!String.IsNullOrWhiteSpace(request.LsPublisherId))
            urlBuilder.Append(String.Format("&lsPublisherId={0}", request.LsPublisherId));
         urlBuilder.Append("&format=xml");

         var serverResponse = XDocument.Load(urlBuilder.ToString());

         var item = (from result in serverResponse.Descendants("item")
                     select result);

         response.ResponseItem = CreateResponseItems(item).FirstOrDefault();
         return response;
      }
      #endregion
      #region Helpers
      private List<ResponseItem> CreateResponseItems(IEnumerable<XElement> items) {
         var responseItems = new List<ResponseItem>();
         foreach (var item in items) {
            var responseItem = new ResponseItem();

            var itemIdValue = (from result in item.Descendants("itemId")
                               select result.Value).FirstOrDefault();
            Int32.TryParse(itemIdValue, out responseItem.ItemId);

            var parentItemIdValue = (from result in item.Descendants("parentItemId")
                                     select result.Value).FirstOrDefault();
            Int32.TryParse(parentItemIdValue, out responseItem.ParentItemId);

            responseItem.Name = (from result in item.Descendants("name")
                                 select result.Value).FirstOrDefault();

            var salePriceValue = (from result in item.Descendants("salePrice")
                                  select result.Value).FirstOrDefault();
            Double.TryParse(salePriceValue, out responseItem.SalePrice);

            responseItem.CategoryPath = (from result in item.Descendants("categoryPath")
                                         select result.Value).FirstOrDefault();

            responseItem.ShortDescription = (from result in item.Descendants("shortDescription")
                                             select result.Value).FirstOrDefault();

            responseItem.LongDescription = (from result in item.Descendants("longDescription")
                                            select result.Value).FirstOrDefault();

            responseItem.ThumbnailImageUrl = (from result in item.Descendants("thumbnailImage")
                                              select result.Value).FirstOrDefault();

            responseItem.ProductTrackingUrl = (from result in item.Descendants("productTrackingUrl")
                                               select result.Value).FirstOrDefault();

            var standardShipRateValue = (from result in item.Descendants("standardShipRate")
                                         select result.Value).FirstOrDefault();
            Double.TryParse(standardShipRateValue, out responseItem.StandardShipRate);

            var marketplaceValue = (from result in item.Descendants("marketplace")
                                    select result.Value).FirstOrDefault();
            Boolean.TryParse(marketplaceValue, out responseItem.MarketPlace);

            responseItem.ProductUrl = (from result in item.Descendants("productUrl")
                                       select result.Value).FirstOrDefault();

            responseItem.CategoryNode = (from result in item.Descendants("categoryNode")
                                         select result.Value).FirstOrDefault();

            var bundleValue = (from result in item.Descendants("bundle")
                               select result.Value).FirstOrDefault();
            Boolean.TryParse(bundleValue, out responseItem.Bundle);

            var availableOnlineValue = (from result in item.Descendants("availableOnline")
                                        select result.Value).FirstOrDefault();
            Boolean.TryParse(availableOnlineValue, out responseItem.AvailableOnline);

            responseItems.Add(responseItem);
         }
         return responseItems;
      }
      #endregion
   }
}
