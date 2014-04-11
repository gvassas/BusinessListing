using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class OrganizationRepository
    {
        public IEnumerable<Organization> GetAllListings()
        {
            BusinessListingsEntities db = new BusinessListingsEntities();

                return (db.Organizations.ToList());
        }


        public Organization ViewListing(int id)
        {
            BusinessListingsEntities db = new BusinessListingsEntities();
            var listing = db.Organizations.Where(x => x.ID == id).FirstOrDefault();
            return listing;
        }

        public string AddListing(string name, string fName, string lName, string phone, string address, string city, string province, string country, string postalCode, float latitude, float longitude)
        {
            string message = CheckExistingListings(name, phone, latitude, longitude);
            BusinessListingsEntities db = new BusinessListingsEntities();
            List<Organization> listings = db.Organizations.ToList();
            if (message == "")
            {
                Organization organization = new Organization();
                organization.Name = name;
                organization.Contact_fName = fName;
                organization.Contact_lName = lName;
                organization.Phone = phone;
                organization.Street_Address = address;
                organization.City = city;
                organization.Province = province;
                organization.Postal_Code = postalCode;
                organization.Country = country;
                organization.latitude = latitude;
                organization.longitude = longitude;
                if (listings.Count > 0)
                {
                    Organization lastOrg = listings.Last();
                    organization.ID = lastOrg.ID + 1;
                }
                else if (listings.Count == 0)
                {
                    organization.ID = 1;
                }

                db.Organizations.Add(organization);
                db.SaveChanges();
                message = "Your listing has been added successfully";
            }
            return message;
            }
        

        public string CheckExistingListings(string name, string phone, float latitude, float longitude)
        {
            string message = "";
            BusinessListingsEntities db = new BusinessListingsEntities();
            Organization listings = db.Organizations.Where(x => x.latitude == latitude && x.longitude ==longitude && x.Name == name && x.Phone ==  phone).FirstOrDefault();

            if (listings != null)
            {
                message = "A duplicate record exists";
            }
            return message;
        }

        public string RemoveListing(int id)
        {
            string message = "";
            BusinessListingsEntities db = new BusinessListingsEntities();
            var Listing = db.Organizations.Where(x => x.ID == id).FirstOrDefault();
            if (Listing != null)
            {
                db.Organizations.Remove(Listing);
                db.SaveChanges();
                message = "Your listing has been successfully removed";
            }
            return message;
        }

        public const string NAME = "Name";
        public const string NAME_DESC = "Name_desc";
        public const string COUNTRY = "Country";
        public const string COUNTRY_DESC = "Country_desc";
        public const string PROVINCE = "Province";
        public const string PROVINCE_DESC = "Province_desc";
        public const string CITY = "City";
        public const string CITY_DESC = "City_desc";

        IEnumerable<Organization> SortOrganization(IEnumerable<Organization> organizations, string sortOrder)
        {
            switch (sortOrder)
            {
                case NAME:
                    organizations = organizations.OrderBy(s => s.Name);
                    break;
                case NAME_DESC:
                    organizations = organizations.OrderByDescending(s => s.Name);
                    break;
                case COUNTRY:
                    organizations = organizations.OrderBy(s => s.Country);
                    break;
                case COUNTRY_DESC:
                    organizations = organizations.OrderByDescending(s => s.Country);
                    break;
                case CITY:
                    organizations = organizations.OrderBy(s => s.City);
                    break;
                case CITY_DESC:
                    organizations = organizations.OrderByDescending(s => s.City);
                    break;
                case PROVINCE:
                    organizations = organizations.OrderBy(s => s.Province);
                    break;
                case PROVINCE_DESC:
                    organizations = organizations.OrderByDescending(s => s.Province);
                    break;
                default:
                    organizations = organizations.OrderBy(s => s.Name);
                    break;
            }
            return organizations;
        }

        public IEnumerable<Organization> GetOrganization(string sortOrder, string searchString)
        {
            BusinessListingsEntities context = new BusinessListingsEntities();
            IEnumerable<Organization> organizations = from s in context.Organizations
                                            select s;
            organizations = SortOrganization(organizations, sortOrder);
            organizations = FilterOrganizations(organizations, searchString);
            return organizations;
        }

        IEnumerable<Organization> FilterOrganizations(IEnumerable<Organization> organizations, string searchString)
        {
            // Filter results based on search.
            if (!String.IsNullOrEmpty(searchString))
                organizations = organizations.Where(
                            s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            return organizations;
        }


    }
}