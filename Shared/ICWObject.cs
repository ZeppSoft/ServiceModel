using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IIDTitle<t>
    {
        t ID { get; set; }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string Title { get; set; }
    }
    public interface IIDTitleObject : IIDTitle<object>
    {

    }

    /// <summary>
    /// Internal CustomWare.NET Object Interface. All Object MUST Implement it.
    /// </summary>
    //[MessagePackFormatter(typeof(TypelessFormatter))]
   // [MessagePackFormatter(typeof(CWObjectFormatter))]
    public interface ICWObject : IIDTitleObject
    {
        /// <summary>
        /// Some ID
        /// </summary>
        //		object ID { get; set; }
        //06.08.2007 OJI We don't need it.
        ///// <summary>
        ///// Link to Relationship in RelationshipsManager
        ///// </summary>
        //IRelationship Relationship { get; set;}
        /// <summary>
        /// is save to the database required
        /// </summary>
        bool IsDirty { get; }
        /// <summary>
        /// reset isDirty variable
        /// </summary>
        void SetUpdated();
        void SetUpdated(bool value);
        /// <summary>
        /// Some title
        /// </summary>
        //        string Title { get; set; }
        bool WasRemoved { get; }
    }

    public abstract class CWObject : ICWObject
    {
        public bool IsDirty => throw new NotImplementedException();

        public bool WasRemoved => throw new NotImplementedException();

        public object ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Code { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SetUpdated()
        {
            throw new NotImplementedException();
        }

        public void SetUpdated(bool value)
        {
            throw new NotImplementedException();
        }
    }
    [MessagePackObject]
    public class DtoCWObject : ICWObject
    {
        [Key(0)]
        public bool IsDirty => true;
        [Key(1)]
        public bool WasRemoved => false;
        [Key(2)]
        public object ID { get; set; }
        [Key(3)]
        public string Title { get; set; }
        [Key(4)]
        public string Code { get ; set; }

        public void SetUpdated()
        {
            throw new NotImplementedException();
        }

        public void SetUpdated(bool value)
        {
            throw new NotImplementedException();
        }
     
    }

    public class TestCWObject : ICWObject, IHaveCode
    {
        public bool IsDirty => true;

        public bool WasRemoved => false;

        public object ID { get; set; }
        public string Title { get; set; }
        public string Code { get ; set; }

        public void SetUpdated()
        {
            throw new NotImplementedException();
        }

        public void SetUpdated(bool value)
        {
            int x = 3 + 3;
        }
    }
}
