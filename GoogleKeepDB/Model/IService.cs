using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepDB.Model
{
    public interface IService
    {
        IEnumerable<Keep> GetKeep();
        Keep Create(Keep p);
        Keep GetKeepByID(ObjectId id);
        IEnumerable<Keep> GetKeepByTitle(string title);
        IEnumerable<Keep> GetKeepByLabel(string label);
        IEnumerable<Keep> GetKeepByPinned(bool ispinned);
        void Update(ObjectId id, Keep p);
        void Remove(ObjectId id);

    }
}
