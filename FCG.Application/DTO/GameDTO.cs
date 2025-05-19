namespace FCG.Application.DTO
{
    public class GameDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required decimal Price { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
    }
}
