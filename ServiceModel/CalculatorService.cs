using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel
{
    //internal sealed class CalculatorDouble : ICalculator<double>
    //{
    //    public List<OT> SearchGeneric<OT>(OT sample) where OT : ICWObject
    //    {
    //        throw new NotImplementedException();
    //    }

    //    // POST: /ICalculator-Double/Sum

    //    double ICalculator<double>.Sum(double x, double y)
    //    {
    //        return x + y;
    //    }
    //}

    //internal sealed class CalculatorNullableInt32 : ICalculator<int?>
    //{
    //    public List<OT> SearchGeneric<OT>(OT sample) where OT : ICWObject
    //    {
    //        throw new NotImplementedException();
    //    }

    //    // POST: /ICalculator-Nullable-Int32/Sum

    //    int? ICalculator<int?>.Sum(int? x, int? y)
    //    {
    //        return x + y;
    //    }
    //}

    internal sealed class CalculatorNullableInterfaceService : ICalculator<ICWObject> 
    {
        public string Code { get ; set; }

        public List<ICWObject> SearchGeneric(ICWObject sample)
        {
            return new List<ICWObject> {new TestCWObject { ID = 1, Title = "Test" } };
        }

        public ICWObject Sum(ICWObject x, ICWObject y)
        {
            return new TestCWObject {ID = 1, Title ="Test" };
        }

        // POST: /ICalculator-Nullable-Int32/Sum
    }
}
