using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IPBIRepository PBIs { get; }
        Task<bool> UpInsertAsync(PBIEntity pbiEntity);
    }
}