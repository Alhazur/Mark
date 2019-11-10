using Mark.Models.Class;
using System.Collections.Generic;

namespace Mark.Models.InterFace
{
    public interface ICompaniesService
    {

        Companies CreateCompany(string name, int organizationNumber, string note);

        List<Companies> AllCompanies();

        Companies FindCompany(int id);

        bool UpDateCompany(Companies companies);

        bool DeleteCompany(int id);
    }
}
