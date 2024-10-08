﻿using Domain.Interfaces.Repositories;
using Domain.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                        .Include(c => c.Home)
                        .Include(c => c.Office)
                        .Include(c => c.FavoriteColors)
                        .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                            .Include(c => c.Home)
                            .Include(c => c.Office)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerByExternalIdAsync(int id)
        {
            return await _context.Customers
                            .Include(c => c.Home)
                            .Include(c => c.Office)
                            .Include(c => c.FavoriteColors)
                            .FirstOrDefaultAsync(c => c.ExternalId == id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);

        }
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }
    }
}
