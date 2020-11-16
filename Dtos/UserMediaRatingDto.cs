namespace JournalistTierAPI.Dtos
{
    public class UserMediaRatingDto
    {
        public int UserId { get; set; }

        public int MediaId { get; set; }

        public int TopicId { get; set; }

        public int Rating { get; set; }
    }

}