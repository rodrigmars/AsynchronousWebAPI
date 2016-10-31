using System;

namespace WorldMusic.Domain.Entities
{
    public class Discography
    {
        public int DiscographyId { get; set; }

        public string Genres { get; set; }

        public string Instrumentalist { get; set; }

        public string Instruments { get; set; }

        public string LastAlbum { get; set; }

        public DateTime Released { get; set; }
    }
}
