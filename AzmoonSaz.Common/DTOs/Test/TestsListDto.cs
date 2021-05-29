namespace AzmoonSaz.Common.DTOs.Test
{
    public record TestsListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int QuestionsCount { get; set; }
    }

}
