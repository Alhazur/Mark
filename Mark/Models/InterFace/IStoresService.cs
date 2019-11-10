using Mark.Models.Class;
using System.Collections.Generic;

namespace Mark.Models.InterFace
{
    public interface IStoresService
    {
        //Stores CreateStore(string name, string address, string city, int zip, string country);
        Stores CreateStore(Stores stores, int companyId);

        List<Stores> AllStores();

        Stores FindStore(int id);

        bool UpDateStore(Stores stores, int? id);

        bool DeleteStore(int id);
    }
}
