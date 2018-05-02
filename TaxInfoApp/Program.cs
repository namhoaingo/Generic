using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxInfoApp.Manager;

namespace TaxInfoApp
{
    class Program
    {
        // This will be the controller
        static void Main(string[] args)
        {
            IReadOnlyTaxManager<TaxInfo> readOnlyManagerDefault = GetReadOnlyTaxManager(1);
            IReadOnlyTaxManager<TaxInfo> readOnlyManagerChina = GetReadOnlyTaxManager(2);
            IReadOnlyTaxManager<TaxInfo> readOnlyManagerTaiwan = GetReadOnlyTaxManager(3);

            TaxInfo defaultTaxInfo = GetTaxInfo(readOnlyManagerDefault);
            TaxInfo taiwanTaxInfo = GetTaxInfo(readOnlyManagerTaiwan);
            TaxInfo chinaTaxInfo = GetTaxInfo(readOnlyManagerChina);

            SaveTaxInfo(1, defaultTaxInfo);
            SaveTaxInfo(2, chinaTaxInfo);
            SaveTaxInfo(3, taiwanTaxInfo);

        }

        // Since IreadOnlyTaxManager has the out keyword
        // I can cast the TaxInfo to the base in TaiwanTaxManager<TaiwanTaxInfo>
        // The out keyword makes sure that I dont add to the list
        // It is only for get the item
        // Because in get Item, I basicly get a based object out regardless of the save type is
        // It is safer to do it that way 
        public static IReadOnlyTaxManager<TaxInfo> GetReadOnlyTaxManager(int country) 
        {
            switch (country)
            {
                case 1:
                    return new DefaultReadOnlyTaxManager<TaxInfo>();
                case 2:
                    return new ChinaReadOnlyTaxManager<ChinaTaxInfo>();
                default:
                    return new TaiwanReadOnlyTaxManager<TaiwanTaxInfo>();
            }

        }

        //public static IWriteOnlyTaxManager<TaxInfo> GetWriteOnlyTaxManager(int country)
        //{
        //    switch (country)
        //    {
        //        case 1:
        //            return new DefaultWriteOnlyTaxManager<TaxInfo>();
        //        case 2:
        //            return new ChinaWriteOnlyTaxManager<ChinaTaxInfo>();
        //        default:
        //            return new TaiwanWriteOnlyTaxManager<TaxInfo>();
        //    }


        //}

        public static void SaveTaxInfo(int country, TaxInfo taxInfo)
        {
            switch (country)
            {
                case 1:
                    (new DefaultWriteOnlyTaxManager<TaxInfo>()).SaveTaxInfo(taxInfo);
                    break;
                case 2:
                    (new ChinaWriteOnlyTaxManager<ChinaTaxInfo>()).SaveTaxInfo((ChinaTaxInfo)(taxInfo));
                    break;
                default:
                    (new TaiwanWriteOnlyTaxManager<TaiwanTaxInfo>()).SaveTaxInfo((TaiwanTaxInfo)taxInfo);
                    break;
            }


        }

        public static TaxInfo GetTaxInfo(IReadOnlyTaxManager<TaxInfo> taxManager) 
        {
            return taxManager.GetTaxInfo();
        }
        
        
    }
}



