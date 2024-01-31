using Newtonsoft.Json;
using System.Collections.Generic;

namespace HttpClientRequests.Models;

public class Tracks
{
    public long TrackId { get; set; }
    public string Name { get; set; }
    public long? AlbumId { get; set; }
    public long? GenreId { get; set; }
    public string Composer { get; set; }
    public long? Milliseconds { get; set; }
    public long? Bytes { get; set; }
    public string UnitPrice { get; set; }
    
    public Albums Album { get; set; }
    public Genres Genre { get; set; }
}
