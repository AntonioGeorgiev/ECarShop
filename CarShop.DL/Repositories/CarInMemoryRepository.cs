using ECarShop.DL.InMemoryDb;
using ECarShop.DL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.DL.Repositories
{
    public class CarInMemoryRepository : ICarRepository
    {
        public CarInMemoryRepository()
        {

        }
        public Car Create(Car car)
        {
            CarInMemoryCollection.CarDb.Add(car);

            return car;
        }

        public Car Delete(int id)
        {
            var car = CarInMemoryCollection.CarDb.FirstOrDefault(car => car.Id == id);

            CarInMemoryCollection.CarDb.Remove(car);

            return car;
        }

        public IEnumerable<Car> GetAll()
        {
            return CarInMemoryCollection.CarDb;
        }

        public Car GetById(int id)
        {
            return CarInMemoryCollection.CarDb.FirstOrDefault(b => b.Id == id);
        }

        public Car Update(Car car)
        {
            var result = CarInMemoryCollection.CarDb.FirstOrDefault(b => b.Id == car.Id);

            result.Id = car.Id;
            result.Brand = car.Brand;
            result.Model = car.Model;
            result.Price = car.Price;


            return result;
        }
    }
}
