namespace Foodily.ViewModels
{
    public class RecipeVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Photo { get; set; }
        public string? Ingredients { get; set; }
        public string? Instruction { get; set; }
        public string? Tags { get; set; }
        public string? Description { get; set; }
        public string? Cooktime { get; set; }
        public string? Preptime { get; set; }
        public string? Difficulty { get; set; }
    }
}
