using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface IRepository<T, U>
{
    Task<U>Create(T entity);
    Task<T?> GetById(U id);
    Task<ICollection<T>>GetAll();
    Task Update(T updatedEntity);
    Task Delete(T entity);
}
