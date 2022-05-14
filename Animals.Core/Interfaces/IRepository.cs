﻿using Animals.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IRepository
    {
        ValueTask<IEnumerable<Animal>> FetchAll();
        ValueTask<bool> Create(Animal animal);
        ValueTask<bool> Delete(int id);
    }
}
