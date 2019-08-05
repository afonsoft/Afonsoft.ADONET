using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Afonsoft.ADONET
{
    public class ParameterCollection : ICollection<IDataParameter>
    {
        private readonly List<IDataParameter> _entries = new List<IDataParameter>();

        /// <summary>
        /// Array Parameter
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IDataParameter this[int index] => _entries[index];
        public IDataParameter this[string parameterName]
        {
            get
            {
                return _entries.FirstOrDefault(a => a.ParameterName == parameterName);
            }
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IDataParameter> GetEnumerator()
        {
            return ((IEnumerable<IDataParameter>)_entries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<Parameter>

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="item"></param>
        public void Add(IDataParameter item)
        {
            _entries.Add(item);
        }

        
        public void AddRange(IDataParameter[] values)
        {
            _entries.AddRange(values);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _entries.Clear();
        }

        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IDataParameter item)
        {
            return _entries.Contains(item);
        }

        /// <summary>
        /// CopyTo
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IDataParameter[] array, int arrayIndex)
        {
            _entries.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IDataParameter item)
        {
            return _entries.Remove(item);
        }

        /// <summary>
        /// Count 
        /// </summary>
        public int Count => _entries.Count;

        /// <summary>
        /// IsReadOnly 
        /// </summary>
        public bool IsReadOnly => false;

        #endregion

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public IDataParameter Find(Predicate<IDataParameter> match)
        {
            return _entries.Find(match);
        }

        /// <summary>
        /// FindAll
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<IDataParameter> FindAll(Predicate<IDataParameter> match)
        {
            return _entries.FindAll(match);
        }
    }
}
