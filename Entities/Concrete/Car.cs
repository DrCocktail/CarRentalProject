﻿using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity //Car bir veritabanı tablosudur.
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
        public decimal DailyPrice { get; set; }

    }
}
