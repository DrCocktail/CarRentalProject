﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental entity)
        {
            _rentalDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Listed);
        }

        public IDataResult<Rental> GetByRentalId(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(b => b.RentalId == id), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<RentDetailDto>> GetRentDetails()
        {
            var getRentalDetails = _rentalDal.GetRentDetails();
            return new SuccessDataResult<List<RentDetailDto>>(getRentalDetails);
        }

        public IDataResult<List<Rental>> GetRentalByCarId(int carId)
        {
            var getRentalByCarId = _rentalDal.GetAll(rental => rental.CarId == carId);
            return new SuccessDataResult<List<Rental>>(getRentalByCarId);
        }

        private IResult CarRentedCheck(Rental rental)
        {
            var rentalledCars = _rentalDal.GetAll(
                r => r.CarId == rental.CarId && (
                r.ReturnDate == null ||
                r.ReturnDate < DateTime.Now)).Any();

            if (rentalledCars)
                return new ErrorResult(Messages.CarIsStillRentalled);

            return new SuccessResult();
        }

        public IDataResult<List<RentDetailDto>> GetRentalDetail()
        {
            return new SuccessDataResult<List<RentDetailDto>>(_rentalDal.GetRentDetails());
        }
    }
}
