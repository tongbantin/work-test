using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryShadowSystem.Collateral
{
    public interface IController<K, V> 
        where K : class
        where V : class
    {
        void GetAll(K filter);
        void GetById(int Id);
        void Remove(int Id);
        void Add(V data);
        void Update(V data);
    }
}
