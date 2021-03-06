﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentDetailDto> GetRentDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from rental in context.Rentals
                             join car in context.Cars
                             on rental.CarId equals car.Id
                             join customer in context.Customers
                             on rental.CustomerId equals customer.CustomerId
                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId
                             join user in context.Users
                             on customer.UserId equals user.UserId
                             select new RentDetailDto
                             {
                                 Id = rental.RentalId,
                                 CarBrand = brand.BrandName,
                                 CarModel = car.Description,
                                 CustomerFirstName = user.FirstName,
                                 CustomerLastName = user.LastName,
                                 CompanyName = customer.CompanyName,
                                 RentDate = (DateTime)rental.RentDate,
                                 ReturnDate = (DateTime)rental.ReturnDate
                             };

                return result.ToList();
            }
        }
    }
}
