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
            IReadOnlyTaxManager<TaxInfo> taxInfoManager = GetTaxManager(1);    
            IReadOnlyTaxManager<TaxInfo> taxInfoManager3 = GetTaxManager(3);

            //ITaxManager<TaiwanTaxInfo> taxInfoManager3 = GetTaxManager(3);
            Test(taxInfoManager);
            Test(taxInfoManager3);

        }


        public static IReadOnlyTaxManager<TaxInfo> GetTaxManager(int country) 
        {
            switch (country)
            {
                case 1:
                    return new DefaultTaxManager<TaxInfo>();
                default:
                    return new TaiwantTaxManager<TaiwanTaxInfo>();
            }

        }

        public static void Test(IReadOnlyTaxManager<TaxInfo> taxManager) 
        {
            Console.WriteLine(taxManager.GetTaxInfo().GetType());
        }

        //public Taxfac
    }
}



