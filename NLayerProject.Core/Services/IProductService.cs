using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Services
{
   public interface IProductService:IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId); //veritabanı ile ilgili işlem
        // bool ControlInnerBarcode(Product product); iç metot veritabanı işlemi değil kontrol işlemi
    }
}
