namespace AzmoonSaz.Domain.Entities.Test
{
    public record Question
    {
        public Question()
        {

        }

        public int Id { get; set; }
        
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public string ImageName { get; set; }
        public string Text { get; set; }

        public string Answers { get; set; }
        
        public int CorrectAnswer { get; set; }
    }
}
