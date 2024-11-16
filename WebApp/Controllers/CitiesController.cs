using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class CitiesController : BaseController
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }



    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        List<City> GetAllCities()
        {
            return new List<City>
        {
           new City(1, "Bakı"),
            new City(2, "Sumqayıt"),
            new City(3, "Gəncə"),
            new City(4, "Mingəçevir"),
            new City(5, "Şirvan"),
            new City(6, "Lənkəran"),
            new City(7, "Şəki"),
            new City(8, "Yevlax"),
            new City(9, "Naxçıvan"),
            new City(10, "Quba"),
            new City(11, "Zaqatala"),
            new City(12, "Şamaxı"),
            new City(13, "Xırdalan"),
            new City(14, "Qəbələ"),
            new City(15, "Qazax"),
            new City(16, "Masallı"),
            new City(17, "Cəlilabad"),
            new City(18, "İsmayıllı"),
            new City(19, "Astara"),
            new City(20, "Tərtər"),
            new City(21, "Balakən"),
            new City(22, "Beyləqan"),
            new City(23, "Füzuli"),
            new City(24, "Saatlı"),
            new City(25, "Sabirabad"),
            new City(26, "Biləsuvar"),
            new City(27, "Hacıqabul"),
            new City(28, "Naftalan"),
            new City(29, "Ucar"),
            new City(30, "Ağdam"),
            new City(31, "Ağcabədi"),
            new City(32, "Ağdaş"),
            new City(33, "Bərdə"),
            new City(34, "Qobustan"),
            new City(35, "Xaçmaz"),
            new City(36, "Şəmkir"),
            new City(37, "Goranboy"),
            new City(38, "Qusar"),
            new City(39, "Salyan"),
            new City(40, "Lerik"),
            new City(41, "Zərdab"),
            new City(42, "Şuşa"),
            new City(43, "Laçın"),
            new City(44, "Kəlbəcər"),
            new City(45, "Qubadlı"),
            new City(46, "Cəbrayıl"),
            new City(47, "Zəngilan"),
            new City(48, "Xocalı"),
            new City(49, "Xocavənd"),
            new City(50, "Ağstafa")
        };
        }

        List<City> cities = GetAllCities();
        return Ok(cities);
    }
}
