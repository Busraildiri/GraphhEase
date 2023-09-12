using System.Collections.Generic;

namespace GraphEase.Models
{
    public class AnalysisResultsViewModel
    {
      

        public List<SalesByCityProductAndGender>? CityProductGenderSalesData { get; set; }
        public List<SalesByGender>? GenderSalesData { get; set; }
        public List<SalesByCityAndGender>? CityGenderSalesData { get; set; }
        public List<SalesByProductName>? ProductSalesData { get; set; }
        public List<SalesByCityAndProduct>? CityProductSalesData { get; set; }
        public List<ProfitableProduct>? ProfitData { get; set; }
        public List<SalesByCityGenderAndCategory>? CityGenderCategorySalesData { get; set; }
    }
    namespace GraphEase.Models
    {
        public class SalesByGender
        {
            public string? Gender { get; set; }
            public int TotalSales { get; set; }
        }


        public class SalesByCityAndGender
        {
            public string? City { get; set; }
            public int MaleSales { get; set; }
            public int FemaleSales { get; set; }
            public int TotalSales { get; set; }
        }

        public class SalesByProductName
        {
            public string? ProductName { get; set; }
            public int TotalSales { get; set; }
        }

        public class CityProductSales
        {
            public string? City { get; set; }
            public string? ProductName { get; set; }
            public int TotalSales { get; set; }
        }

        public class ProfitableProduct
        {
            public string? ProductName { get; set; }
            public int Profit { get; set; }
        }

        public class SalesByCityProductAndGender
        {
            public string? City { get; set; }
            public string? ProductName { get; set; }
            public int MaleSales { get; set; }
            public int FemaleSales { get; set; }
        }

        public class SalesByCityGenderAndCategory
        {
            public string? City { get; set; }
            public string? Gender { get; set; }
            public string? Category { get; set; }
            public int SalesCount { get; set; }
        }
    }
}
