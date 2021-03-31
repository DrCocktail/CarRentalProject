﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetByCustomerId(int id);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IDataResult<CustomerDetailDto> GetByEmail(string email);
    }
}
