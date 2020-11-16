namespace JournalistTierAPI.Dtos
{
    public class UserJournalistRatingDto
    {
        public int UserId { get; set; }

        public int JournalistId { get; set; }

        public int TopicId { get; set; }

        public int Rating { get; set; }
    }

}