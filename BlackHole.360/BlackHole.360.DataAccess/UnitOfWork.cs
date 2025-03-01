﻿using BlackHole._360.DataAccess.Repositories;
using BlackHole._360.Domain.Entities;
using BlackHole._360.Domain.Interfaces;
using BlackHole._360.Domain.Interfaces.Entities;
using BlackHole._360.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlackHole._360.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    private readonly BlackHoleContext _context;

    private IRepository<User> _employeeRepository;

    public IRepository<User> EmployeeRepository => _employeeRepository ??= new Repository<User>(_context);


    public UnitOfWork(BlackHoleContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        try
        {
            PerformExtraOperations();

            return _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // TODO: Log ex
            throw ex;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            PerformExtraOperations();

            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // TODO: Log ex
            throw ex;
        }
    }

    private void PerformExtraOperations()
    {
        var entries = _context.ChangeTracker.Entries<ITimeTracked>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
            }
        }
    }

    #region Implementation of IDisposable
    private bool isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
