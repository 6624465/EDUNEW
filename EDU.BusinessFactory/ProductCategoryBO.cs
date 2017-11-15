
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.EDU.Contract;
using EZY.EDU.DataFactory;

namespace EZY.EDU.BusinessFactory
{
    public class ProductCategoryBO
    {
        private ProductCategoryDAL productcategoryDAL;
        public ProductCategoryBO()
        {

            productcategoryDAL = new ProductCategoryDAL();
        }

        public List<ProductCategoryMaster> GetList()
        {
            return productcategoryDAL.GetList();
        }

        

        public bool SaveProduct(ProductCategoryMaster newItem)
        {

            return productcategoryDAL.Save(newItem);

        }

        public bool DeleteProduct(ProductCategoryMaster item)
        {
            return productcategoryDAL.Delete(item);
        }



        public ProductCategoryMaster GetProductCategory(ProductCategoryMaster item)
        {
            return (ProductCategoryMaster)productcategoryDAL.GetItem<ProductCategoryMaster>(item);
        }

    }
}

