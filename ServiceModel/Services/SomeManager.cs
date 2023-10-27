using MessagePack;
using Shared;
using Shared.Interfaces;
using Shared.Objects;
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

        public Complex GetComplexObject()
        {
            return new Complex();
        }

        public ICWObject GetCWObject(string la)
        {
            return new TestCWObject { ID = 1, Code = "ASM", Title = "Obj" };
        }

        public string getName(int personId)
        {
            return $"Hello, {personId}!";
        }

        public IList GetPenalties(string la)
        {
            //List<PenaltyAmount> ras = new List<PenaltyAmount>();

            //ras.Add(new PenaltyAmount { Amount = 133, Tax = 233 });
            //ras.Add(new PenaltyAmount { Amount = 333, Tax = 433 });
            //ras.Add(new PenaltyAmount { Amount = 533, Tax = 633 });
            //ras.Add(new PenaltyAmount { Amount = 733, Tax = 833 });
            //ras.Add(new PenaltyAmount { Amount = 933, Tax = 1033 });

            List<TestCWObject> ras = new List<TestCWObject>();
            ras.Add(new TestCWObject { Code = la, ID = 31, Title = "New" });
            ras.Add(new TestCWObject { Code = la, ID = 32, Title = "New2" });
            ras.Add(new TestCWObject { Code = la, ID = 33, Title = "New3" });


            return ras;
        }

        public PenaltyAmount GetPenalty(string la)
        {
            return new PenaltyAmount { Amount = 333, Tax= 50 };
        }

        public TestPerson GetTestPerson()
        {
           return new TestPerson() {FirstName ="John",LastName = "Tiner" };
        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
