using MessagePack;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Services
{
    public class SomeManager : ISomeManager
    {
        public IList GetAmounts(string la)
        {
            List<RepaymentAmount> ras = new List<RepaymentAmount>();

            ras.Add(new RepaymentAmount {Amount = 10,Tax = 20 });
            ras.Add(new RepaymentAmount { Amount = 20, Tax = 30 });
            ras.Add(new RepaymentAmount { Amount = 30, Tax = 40 });
            ras.Add(new RepaymentAmount { Amount = 40, Tax = 50 });
            ras.Add(new RepaymentAmount { Amount = 50, Tax = 60 });

            return ras;

        }

        public ICWObject GetCWObject(string la)
        {
            return new TestCWObject { ID = 1, Code = "ASM", Title = "Obj" };
        }

        public string getName(int personId)
        {
            return $"Hello, {personId}!";
        }

        public PenaltyAmount GetPenalty(string la)
        {
            return new PenaltyAmount { Amount = 333, Tax= 50 };
        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
