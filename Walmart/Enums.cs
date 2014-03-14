namespace Walmart {
   public enum CategoryId {
      Apparel = 5438,
      AutoAndTires = 91083,
      Baby = 5427,
      Beauty = 1085666,
      Books = 3920,
      CellPhones = 1105910,
      Electronics = 3944,
      GiftsAndRegistry = 1094765,
      Grocery = 976759,
      Health = 976760,
      Home = 4044,
      HomeImprovement = 1072864,
      HouseholdEssentials = 1115193,
      Jewelry = 3891,
      MoviesAndTv = 4096,
      Music = 4104,
      PartyAndOccasions = 2637,
      PatioAndGarden = 5428,
      Pets = 5440,
      PhotoCenter = 5426,
      Seasonal = 1085632,
      SportsAndOutdoors = 4125,
      Toys = 4171,
      TravelAndVacations = 5429,
      VideoGames = 2636
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
