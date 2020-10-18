using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.UnitOfWorks
{
   public interface IUnitOfWork
    {
        //biz bu metotları çağırdığımız zaman savechanges olacak.Sıralı işlemlerde kullanılır 

        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        Task CommitAsync();

        void Commit();
    }
}
