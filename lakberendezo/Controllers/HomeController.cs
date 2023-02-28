using lakberendezo.Models;
using lakberendezo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lakberendezo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static RoomModel? room = new RoomModel();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Process(int width, int height)
        {
            string[,] matrix = new string[width, height];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = string.Empty;
                }
            }

            room.Matrix = matrix;
            room.Width = width;
            room.Height = height;
            return View("Furniture");
        }
        [HttpPost]
        public IActionResult ProcessFurniture(string name, int width, int height)
        {
            var furniture = new FurnitureViewModel
            {
                Name = name,
                Width = width,
                Height = height
            };

            for (int i = 0; i <= room.Matrix.GetLength(0) - furniture.Width; i++)
            {
                for (int j = 0; j <= room.Matrix.GetLength(1) - furniture.Height; j++)
                {
                    if (IsFurnitureFitsAtPosition(furniture, i, j))
                    {
                        PlaceFurnitureAtPosition(furniture, i, j);
                        return View("Room", room);
                    }
                }
            }

            return View("Room", room);
        }

        private bool IsFurnitureFitsAtPosition(FurnitureViewModel furniture, int i, int j)
        {
            for (int p = 0; p < furniture.Width; p++)
            {
                for (int k = 0; k < furniture.Height; k++)
                {
                    var x = i + p;
                    var y = j + k;

                    if (room.Matrix[x, y] != null && room.Matrix[x, y] != string.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void PlaceFurnitureAtPosition(FurnitureViewModel furniture, int i, int j)
        {
            for (int z = 0; z < furniture.Width; z++)
            {
                for (int x = 0; x < furniture.Height; x++)
                {
                    room.Matrix[i + z, j + x] = furniture.Name;
                }
            }
        }

        [HttpPost]
        public IActionResult ProcessBackFromRoomToFurniture()
        {
            return View("Furniture");
        }
    }
}