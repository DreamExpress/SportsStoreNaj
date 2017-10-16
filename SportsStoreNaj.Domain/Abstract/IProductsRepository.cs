﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreNaj.Domain.Entities;
namespace SportsStoreNaj.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }
}
