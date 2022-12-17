using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiForExamMobile.Models
{
    public class ModelTable
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public int StockAvailability { get; set; }
        public int AvailabilityInTheStore { get; set; }
        public string Description { get; set; }
        public string Rewiews { get; set; }
        public string Image { get; set; }

    }
    public class classBooks : ModelTable
    {
        public classBooks(Books books)
        {
            ID = books.ID;
            Title = books.Title;
            Cost = (int)books.Cost;
            StockAvailability = (int)books.StockAvailability;
            AvailabilityInTheStore = (int)books.AvailabilityInTheStore;
            Description = books.Description;
            Rewiews = books.Rewiews;
            Image = books.Image;
        }
    }  public class classMovies : ModelTable
    {
        public classMovies(Movies movies)
        {
            ID = movies.ID;
            Title = movies.Title;
            Cost = (int)movies.Cost;
            StockAvailability = (int)movies.StockAvailability;
            AvailabilityInTheStore = (int)movies.AvailabilityInTheStore;
            Description = movies.Description;
            Rewiews = movies.Rewiews;
            Image = movies.Image;
        }
    }  public class classBuildingMaterials : ModelTable
    {
        public classBuildingMaterials(BuildingMaterials buildingMaterialsController)
        {
            ID = buildingMaterialsController.ID;
            Title = buildingMaterialsController.Title;
            Cost = (int)buildingMaterialsController.Cost;
            StockAvailability = (int)buildingMaterialsController.StockAvailability;
            AvailabilityInTheStore = (int)buildingMaterialsController.AvailabilityInTheStore;
            Description = buildingMaterialsController.Description;
            Rewiews = buildingMaterialsController.Rewiews;
            Image = buildingMaterialsController.Image;
        }
    } 
    public class classHatShop : ModelTable
    {
        public classHatShop(HatShop hatShop)
        {
            ID = hatShop.ID;
            Title = hatShop.Title;
            Cost = (int)hatShop.Cost;
            StockAvailability = (int)hatShop.StockAvailability;
            AvailabilityInTheStore = (int)hatShop.AvailabilityInTheStore;
            Description = hatShop.Description;
            Rewiews = hatShop.Rewiews;
            Image = hatShop.Image;
        }
    }  public class classTableGames: ModelTable
    {
        public classTableGames(TableGames tableGames)
        {
            ID = tableGames.ID;
            Title = tableGames.Title;
            Cost = (int)tableGames.Cost;
            StockAvailability = (int)tableGames.StockAvailability;
            AvailabilityInTheStore = (int)tableGames.AvailabilityInTheStore;
            Description = tableGames.Description;
            Rewiews = tableGames.Rewiews;
            Image = tableGames.Image;
        }
    }  public class classWaxFigure : ModelTable
    {
        public classWaxFigure(WaxFigures waxFigure)
        {
            ID = waxFigure.ID;
            Title = waxFigure.Title;
            Cost = (int)waxFigure.Cost;
            StockAvailability = (int)waxFigure.StockAvailability;
            AvailabilityInTheStore = (int)waxFigure.AvailabilityInTheStore;
            Description = waxFigure.Description;
            Rewiews = waxFigure.Rewiews;
            Image = waxFigure.Image;
        }
    }
}