namespace AzmoonSaz.Domain.Entities.User
{
    public record UserInClassroom
    {
        public UserInClassroom()
        {

        }

        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ClassroomId { get; set; }
        public virtual Classroom.Classroom Classroom { get; set; }
    }
}
