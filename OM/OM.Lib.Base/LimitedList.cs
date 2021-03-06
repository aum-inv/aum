﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Lib.Base
{
    [Serializable]
    public class LimitedList<T> : List<T>
    {
        private int limit = -1;

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public LimitedList(int limit)
        {
            this.Limit = limit;
        }

        public void Insert(T item)
        {
            lock (this)
            {
                this.Insert(0, item);

                if (this.Count > limit)
                    this.RemoveAt(limit);
            }
        }
        public List<T> ToList()
        {
            return this.ToList<T>();
        }
    }
}
