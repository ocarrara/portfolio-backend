namespace PortfolioApi.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string ProjectUrl { get; set; } = "";
    }
}
