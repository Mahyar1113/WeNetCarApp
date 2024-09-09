using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

public class CarRepository
{
    private string connectionString = "Data Source=|DataDirectory|\\cars.db;Version=3;";
    private object carRepository;

    public void UpdateCar(Car car)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE Cars SET Location = @Location, Price = @Price WHERE Id = @Id";

            using (var command = new SQLiteCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Location", car.Location);
                command.Parameters.AddWithValue("@Price", car.Price);
                command.Parameters.AddWithValue("@Id", car.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    public void InsertCar(Car car)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string insertQuery = "INSERT INTO Cars (ImageUrl, Name, Description, ProductionDate, Location, Price, Picture) VALUES (@ImageUrl, @Name, @Description, @ProductionDate, @Location, @Price, @Picture)";

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@ImageUrl", car.ImageUrl);
                command.Parameters.AddWithValue("@Name", car.Name);
                command.Parameters.AddWithValue("@Description", car.Description);
                command.Parameters.AddWithValue("@ProductionDate", car.ProductionDate);
                command.Parameters.AddWithValue("@Location", car.Location);
                command.Parameters.AddWithValue("@Price", car.Price);
                command.Parameters.AddWithValue("@Picture", car.Picture);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Car> GetAllCars()
    {
        List<Car> cars = new List<Car>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Cars";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ImageUrl = reader["ImageUrl"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            ProductionDate = reader["ProductionDate"].ToString(),
                            Location = reader["Location"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Picture = reader["Picture"].ToString()
                        };

                        cars.Add(car);
                    }
                }
            }
        }
        return cars;
    }
    

    public Car GetCarById(int id)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Cars WHERE Id = @Id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Car car = new Car
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ImageUrl = reader["ImageUrl"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            ProductionDate = reader["ProductionDate"].ToString(),
                            Location = reader["Location"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Picture = reader["Picture"].ToString()
                        };
                        

                        return car;
                    }
                }
            }
        }
        return null;
   

    }

    public void DeleteCar(Car car)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM Cars WHERE Id = @Id";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", car.Id);
                command.ExecuteNonQuery();
            }
        }
    }
    

        public Dictionary<string, int> GetCarCountByLocation()
        {
            Dictionary<string, int> carCountByLocation = new Dictionary<string, int>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Location, COUNT(*) AS CarCount FROM Cars GROUP BY Location";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string location = reader["Location"].ToString();
                            int carCount = Convert.ToInt32(reader["CarCount"]);

                            carCountByLocation.Add(location, carCount);
                        }
                    }
                }
            }

            return carCountByLocation;
        }
    public Dictionary<string, decimal> GetAveragePriceByLocation()
    {
        Dictionary<string, decimal> averagePriceByLocation = new Dictionary<string, decimal>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Location, AveragePrice FROM AveragePriceByLocation";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string location = reader["Location"].ToString();
                        decimal averagePrice = Convert.ToDecimal(reader["AveragePrice"]);

                        averagePriceByLocation.Add(location, averagePrice);
                    }
                }
            }
        }
        return averagePriceByLocation;
    }
    public List<Car> GetTopExpensiveCars(int count)
    {
        // Implement the logic to retrieve the top expensive cars from your data source
        // and return a list of Car objects representing those cars

        // Example implementation:
        var expensiveCars = new List<Car>();

        // Logic to fetch top expensive cars, such as using LINQ or database query

        return expensiveCars;
    }
}
        
    
