using Mark.DataBase;
using Mark.Models.Class;
using Mark.Models.InterFace;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mark.Models.Service
{
    public class CompanyService : ICompaniesService
    {
        readonly MarkDbContext _markDbContext;
        public CompanyService(MarkDbContext markDbContext)
        {
            _markDbContext = markDbContext;
        }

        public List<Companies> AllCompanies()
        {
            return _markDbContext.Companiesdb
                .ToList();
        }

        public Companies CreateCompany(string name, int organizationNumber, string note)
        {
            Companies company = new Companies()
            {
                Name = name,
                OrganizationNumber = organizationNumber,
                Note = note
            };

            _markDbContext.Companiesdb.Add(company);
            _markDbContext.SaveChanges();
            return company;
        }

        public bool DeleteCompany(int id)
        {
            bool Removed = false;

            Companies company = _markDbContext.Companiesdb
                .Include(c => c.Stores)
                .SingleOrDefault(c => c.Id == id);

            if (company == null)
            {
                return Removed;
            }

            _markDbContext.Companiesdb.Remove(company);
            _markDbContext.SaveChanges();

            return Removed;
        }

        public Companies FindCompany(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return _markDbContext.Companiesdb
                .Include(s => s.Stores)
                .SingleOrDefault(c => c.Id == id);
        }

        public bool UpDateCompany(Companies companies)
        {
            bool UpDated = false;
            Companies company = _markDbContext.Companiesdb
                .Include(S => S.Stores)
                .SingleOrDefault(c => c.Id == companies.Id);

            if (company != null)
            {
                company.Name = companies.Name;
                company.OrganizationNumber = companies.OrganizationNumber;
                company.Note = companies.Note;

                _markDbContext.SaveChanges();
                UpDated = true;
            }
            return UpDated;
        }
    }
}
