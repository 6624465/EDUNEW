using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class LookupBO
    {
        private LookupDAL lookupDAL;
        public LookupBO()
        {

            lookupDAL = new LookupDAL();
        }

        public List<Lookup> GetList()
        {
            return lookupDAL.GetList();
        }


        public bool SaveLookup(Lookup newItem)
        {

            return lookupDAL.Save(newItem);

        }
        public bool AddLookup(Lookup newItem)
        {
            return lookupDAL.Add(newItem);
        }

        public bool DeleteLookup(Lookup item)
        {
            return lookupDAL.Delete(item);
        }

        public Lookup GetLookup(Lookup item)
        {
            return (Lookup)lookupDAL.GetItem<Lookup>(item);
        }

    }
}
