using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepDB.Model
{
    public class dataAcc
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;

        public dataAcc()
        {
            _client = new MongoClient("mongodb://localhost:27017");
#pragma warning disable CS0618 // Type or member is obsolete
            _server = _client.GetServer();
#pragma warning restore CS0618 // Type or member is obsolete
            _db = _server.GetDatabase("Keep");
        }

        // GET: api/keep
        public IEnumerable<Keep> GetKeep()
        {
            return  _db.GetCollection<Keep>("Keep").FindAll();
        }

        // create for post
        public Keep Create(Keep p)
        {
            _db.GetCollection<Keep>("Keep").Save(p);
            return p;
        }

        // get by id
        public Keep GetKeepByID(ObjectId id)
        {
            var res = Query<Keep>.EQ(p => p.Id, id);
            return _db.GetCollection<Keep>("Keep").FindOne(res);
        }

        // get by title
        public IEnumerable<Keep> GetKeepByTitle(string title)
        {
            return _db.GetCollection<Keep>("Keep").FindAll().Where(p => p.title == title);
        }

        // get by label
        public IEnumerable<Keep> GetKeepByLabel(string label)
        {
            return _db.GetCollection<Keep>("Keep").FindAll().Where(p => p.label == label);
        }

        // get by pinned
        public IEnumerable<Keep> GetKeepByPinned(bool ispinned)
        {
            return _db.GetCollection<Keep>("Keep").FindAll().Where(p => p.isPinned == ispinned);
        }

        // update by id for put
        public void Update(ObjectId id, Keep p)
        {
            p.Id = id;
            var res = Query<Keep>.EQ(pd => pd.Id, id);
            var operation = Update<Keep>.Replace(p);
            _db.GetCollection<Keep>("Keep").Update(res, operation);
        }

        // for delete
        public void Remove(ObjectId id)
        {
            var res = Query<Keep>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Keep>("Keep").Remove(res);
        }

    }
}
