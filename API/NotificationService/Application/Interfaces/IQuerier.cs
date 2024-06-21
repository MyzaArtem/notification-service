using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuerier<T> where T : BaseEntity
    {
        IQueryable<T> Query();
    }

}
