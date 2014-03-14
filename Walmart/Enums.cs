using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walmart {
   public enum CategoryId {
   }

   public enum ResponseGroup {
      Base,
      Full
   }

   public enum Sort {
      Relevance,
      Price,
      Title,
      BestSeller,
      CustomerRating,
      New
   }

   public enum Order {
      Asc,
      Dsc
   }
}
