namespace WorldMusic.Domain.Entities
{
    public class Music
    {
        public int MusicId { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }
        public bool IsActive { get; set; }
        public int IDProcess { get; set; }
    }
}
