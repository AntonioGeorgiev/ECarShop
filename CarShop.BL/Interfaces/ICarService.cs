using ECarShop.Models.DTO;
using System.Collections.Generic;

namespace ECarShop.BL.Interfaces
{
    public interface ICarService
    {
        Car Create(Car car);

        Car Update(Car car);

        Car Delete(int id);

        Car GetById(int id);

        IEnumerable<Car> GetAll();
       
    }
}
