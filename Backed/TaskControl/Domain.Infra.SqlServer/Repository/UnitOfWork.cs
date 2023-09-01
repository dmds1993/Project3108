using Domain.Entities;
using Domain.Infra.SqlServer.DependecyInjection;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infra.SqlServer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
            PBIs = new PBIRepository(_context);
        }

        public ITaskRepository Tasks { get; private set; }
        public IPBIRepository PBIs { get; private set; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpInsertAsync(PBIEntity pbiEntity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (pbiEntity.Tasks != null && pbiEntity.Tasks.Any())
                {
                    var existingPBI = await _context.PBIs.SingleOrDefaultAsync(p => p.Name == pbiEntity.Name);

                    if (existingPBI == null)
                    {
                        var newPBI = new PBIEntity { Name = pbiEntity.Name };
                        _context.PBIs.Add(newPBI);
                        await _context.SaveChangesAsync();

                        foreach (var taskModel in pbiEntity.Tasks)
                        {
                            var newTask = new TaskEntity
                            {
                                Name = taskModel.Name,
                                Description = taskModel.Description,
                                Deadline = taskModel.Deadline,
                                Image = taskModel.Image,
                                PBIId = newPBI.Id
                            };
                            _context.Tasks.Add(newTask);
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        foreach (var taskModel in pbiEntity.Tasks)
                        {
                            var newTask = new TaskEntity
                            {
                                Name = taskModel.Name,
                                Description = taskModel.Description,
                                Deadline = taskModel.Deadline,
                                Image = taskModel.Image,
                                PBIId = existingPBI.Id
                            };
                            _context.Tasks.Add(newTask);
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}