using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Walmart {
   public class WalmartSearchClient {
      private const string BaseUrl = "http://walmartlabs.api.mashery.com/v1/";
      private readonly string _apiKey;
      public WalmartSearchClient(string apiKey) {
         if (String.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("apiKey is required");
         _apiKey = apiKey;
      }

      public SearchResponse ItemSearch(SearchRequest request) {
         var urlBuilder = new StringBuilder(String.Format("{0}search?apiKey={1}", BaseUrl, _apiKey));
         if (String.IsNullOrWhiteSpace(request.Keywords))
            throw new InvalidOperationException("Keyword is required");
         urlBuilder.Append(String.Format("&query={0}", request.Keywords));
         if (!String.IsNullOrWhiteSpace(request.LsPublisherId))
            urlBuilder.Append(String.Format("&lsPublisherId={0}", request.LsPublisherId));
         if (request.CategoryId != null)
            urlBuilder.Append(String.Format("&categoryId={0}", request.CategoryId.Value.GetTypeCode()));
         if (request.Start != null)
            urlBuilder.Append(String.Format("&start={0}", request.Start));
         if (request.Sort != null) {
            urlBuilder.Append(String.Format("&sort={0}", request.Sort.Value.ToString()));
            if (request.Order != null)
               urlBuilder.Append(String.Format("&order={0}", request.Order.Value.ToString()));
         }
         urlBuilder.Append("&format=xml");
         if (request.ResponseGroup != null)
            urlBuilder.Append(String.Format("&responseGroup={0}", request.ResponseGroup.Value.ToString()));

         var serverResponse = XmlReader.Create(urlBuilder.ToString());
         return new SearchResponse();
      }
   }
}
