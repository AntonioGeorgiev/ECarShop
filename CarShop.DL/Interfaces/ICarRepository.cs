using ECarShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECarShop.DL.Interfaces
{
    public interface ICarRepository
    {
        Car Create(Car car);

        Car Update(Car car);

        Car Delete(int id);

        Car GetById(int id);

        IEnumerable<Car> GetAll();
    }
}
