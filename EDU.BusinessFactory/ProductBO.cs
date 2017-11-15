using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class ProductBO
    {
        private ProductDAL productDAL;
        public ProductBO()
        {

            productDAL = new ProductDAL();
        }

        public List<Product> GetList()
        {
            return productDAL.GetList();
        }

        public List<Product> GetProductTableDataList(DataTableObject Obj)
        {
            return productDAL.GetProductTableDataList(Obj);
        }


        public bool SaveProduct(Product newItem)
        {

            return productDAL.Save(newItem);

        }

        public bool DeleteProduct(Product item)
        {
            return productDAL.Delete(item);
        }

        public Product GetProduct(Product item)
        {
            return (Product)productDAL.GetItem<Product>(item);
        }

    }
}
