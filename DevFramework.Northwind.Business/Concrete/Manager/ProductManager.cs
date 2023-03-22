using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Core.Aspects.Postsharp;
using DevFramework.Core.Aspects.Postsharp.ValidationAspect;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;

namespace DevFramework.Northwind.Business.Concrete.Manager
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }
        
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p=>p.ProductId==id);
        }
        [TransactionalScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            _productDal.Add(product2);
        }
    }
}
