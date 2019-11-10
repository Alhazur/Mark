using Mark.DataBase;
using Mark.Models.Class;
using Mark.Models.InterFace;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mark.Models.Service
{
    public class StoresService : IStoresService
    {
        readonly MarkDbContext _markDbContext;
        public StoresService(MarkDbContext markDbContext)
        {
            _markDbContext = markDbContext;
        }

        public List<Stores> AllStores()
        {
            return _markDbContext.Storesdb
                .Include(c => c.Companies)//.Include(c => c.Companies)????? many to one
                .ToList();
        }

        public Stores CreateStore(Stores store, int Id)
        {
            var name = _markDbContext.Companiesdb
                .Include(s => s.Stores)
                .SingleOrDefault(c => c.Id == Id);

            //name.stores.add(new stores
            //{
            //    companyid = id,
            //    name = store.name,
            //    city = store.city,
            //    address = store.address,
            //    zip = store.zip,
            //    country = store.country,
            //    latitude = store.latitude,
            //    longitude = store.longitude
            //});

            name.Stores.Add(store);

            _markDbContext.SaveChanges();
            return store;
        }

        public bool DeleteStore(int id)
        {
            bool Removed = false;

            Stores store = _markDbContext.Storesdb
                .SingleOrDefault(s => s.Id == id);
            if (store == null)
            {
                return Removed;
            }
            _markDbContext.Storesdb.Remove(store);
            _markDbContext.SaveChanges();
            return Removed;
        }

        public Stores FindStore(int id)
        {

            return _markDbContext.Storesdb.SingleOrDefault(s => s.Id == id);

        }

        public bool UpDateStore(Stores stores, int? id)
        {
            bool Updated = false;

            Stores store = _markDbContext.Storesdb
                .SingleOrDefault(s => s.Id == stores.Id);

            if (store != null)
            {
                store.Name = stores.Name;
                store.Address = stores.Address;
                store.City = stores.City;
                store.Zip = stores.Zip;
                store.Country = stores.Country;
                store.Longitude = stores.Longitude;
                store.Latitude = stores.Latitude;

                _markDbContext.SaveChanges();
                Updated = true;
            }
            return Updated;
        }
    }
}
