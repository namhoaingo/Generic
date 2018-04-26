using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxInfoApp.Manager
{
    

    public interface ITaxManager<T>: IReadOnlyTaxManager<T>, IWriteOnlyTaxManager<T>
    {

    }

    // This interface will return the T
    public interface IReadOnlyTaxManager<out T> { 
        T GetTaxInfo();
    }

    public interface IWriteOnlyTaxManager<in T> 
    {
        void SaveTaxInfo(T taxInfo);
    }

    //Each country will have their own way of read tax info and write tax info

    public abstract class BaseTaxManager<T> : IReadOnlyTaxManager<T> where T :  TaxInfo, new()
{
        public virtual T GetTaxInfo()
        {            
            return new T();
        }
    }


    public class DefaultTaxManager<T>: BaseTaxManager<T> where T: TaxInfo, new()
    {
        public override T GetTaxInfo()
        {
            var t = base.GetTaxInfo();
            t.CustomerId = "Default";
            return t;
        }
    }

    public class TaiwantTaxManager<T> : DefaultTaxManager<T>, IReadOnlyTaxManager<T> where T : TaiwanTaxInfo, new()
    {
        public override T GetTaxInfo()
        {
            var t = base.GetTaxInfo();
            t.DonnationCode = "Taiwan";
            return t;

        }
    }


    //// Only take care of Taiwan TaxInfo
    //public class TaiwanTaxManager<T>: DefaultTaxManager<T> where T : TaxInfo, new()
    //{
    //    public TaiwanTaxManager()
    //    {

    //    }
    //    public override T GetTaxInfo()
    //    {
    //        // Do it the Taiwanese way
    //        //base.GetTaxInfo();
    //        Console.WriteLine("GetTaxInfo TW");            
    //        return new T();
    //    }

    //    public override void SaveTaxInfo(T taxInfo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override T PopulateTaxInfoObject(string input)
    //    {
    //        //Base on Country to generate TaxObject
    //        throw new NotImplementedException();
    //    }
    //}

       
    public class TaxInfo
    {
        public string Country { get; set; }
        public string CustomerId { get; set; }
    }

    public class TaiwanTaxInfo: TaxInfo
    {
        public TaiwanTaxInfo()
        {

        }
        public string DonnationCode { get; set; }
    }
}
