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

    // The out keyword is only for getting item
    // So I can cast it to the base
    public interface IReadOnlyTaxManager<out T> { 
        T GetTaxInfo();
    }

    //The in keyword is only for adding the item in
    // Si I can cast it to the children
    // Because the item should already be the base class,
    // I should be able to play around with it, or cast it to the child
    public interface IWriteOnlyTaxManager<in T> 
    {
        void SaveTaxInfo(T taxInfo);
    }

    //Each country will have their own way of read tax info and write tax info

    public abstract class BaseReadOnlyTaxManager<T> : IReadOnlyTaxManager<T> where T :  TaxInfo, new()
{
        public virtual T GetTaxInfo()
        {            
            return new T();
        }
    }


    public class DefaultReadOnlyTaxManager<T>: BaseReadOnlyTaxManager<T> where T: TaxInfo, new()
    {
        public override T GetTaxInfo()
        {
            var t = base.GetTaxInfo();
            t.CustomerId = "Default";
            return t;
        }
    }

    public class TaiwanReadOnlyTaxManager<T> : DefaultReadOnlyTaxManager<T>, IReadOnlyTaxManager<T> where T : TaiwanTaxInfo, new()
    {
        public override T GetTaxInfo()
        {
            var t = base.GetTaxInfo();
            t.DonnationCode = "Taiwan";
            return t;

        }
    }


    public class ChinaReadOnlyTaxManager<T> : DefaultReadOnlyTaxManager<T>, IReadOnlyTaxManager<T> where T : ChinaTaxInfo, new()
    {
        public override T GetTaxInfo()
        {
            var t = base.GetTaxInfo();
            t.ChinaID = "China";
            return t;

        }
    }

    public abstract class BaseWriteOnlyTaxManager<T> : IWriteOnlyTaxManager<T> where T : TaxInfo, new()
    {
        public virtual void SaveTaxInfo(T taxInfo)
        {
            throw new NotImplementedException();
        }
    }
    
    public class DefaultWriteOnlyTaxManager<T>: BaseWriteOnlyTaxManager<T> where T: TaxInfo, new()
    {
        public override void SaveTaxInfo(T taxInfo)
        {
            Console.WriteLine(taxInfo.CustomerId);
        }
    }

    public class TaiwanWriteOnlyTaxManager<T> : DefaultWriteOnlyTaxManager<T>, IWriteOnlyTaxManager<T> where T : TaiwanTaxInfo, new()
    {
        public override void SaveTaxInfo(T taxInfo)
        {
            Console.WriteLine(taxInfo.DonnationCode);
        }
    }


    public class ChinaWriteOnlyTaxManager<T> : DefaultWriteOnlyTaxManager<T>, IWriteOnlyTaxManager<T> where T : ChinaTaxInfo, new()
    {
        public override void SaveTaxInfo(T taxInfo)
        {
            Console.WriteLine(taxInfo.ChinaID);
        }
    }

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

    public class ChinaTaxInfo : TaxInfo
    {
        public ChinaTaxInfo()
        {

        }
        public string ChinaID { get; set; }
    }
}
