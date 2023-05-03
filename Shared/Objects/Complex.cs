using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Objects
{
    public static class ComplexHelper
    {
        public static int GetIntValue()
        {
            return -1000;
        }

        public static string GetStringValue() 
        {
            return "Hello";
        }
    }
    [MessagePackObject]
    public class Complex
    {
        private int _intProperty;
        [Key(0)]
        public int IntProp 
        {
            get
            {
              //  if (_intProperty == 0)
                    _intProperty = ComplexHelper.GetIntValue();
                return _intProperty;
            }
            set
            {
                _intProperty = value;
            }
        }

       private string _stringProperty;
        [IgnoreMember]
        public string StringProp 
        {
            get
            {
                _stringProperty = ComplexHelper.GetStringValue();
                return _stringProperty;
                
            }
            set
            {
                _stringProperty = value;
            }
        }
    }
}
