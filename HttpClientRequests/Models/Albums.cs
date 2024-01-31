﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HttpClientRequests.Models;

public class Albums : IEquatable<Albums>
{
    public Albums() => Tracks = new HashSet<Tracks>();

    [JsonProperty(Required = Required.Always)]
    public long AlbumId { get; set; }

    [JsonProperty(Required = Required.AllowNull)]
    public string Title { get; set; }

    [JsonProperty(Required = Required.AllowNull)]
    public long? ArtistId { get; set; }
    
    [JsonProperty(Required = Required.AllowNull)]
    public Artists Artist { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public ICollection<Tracks> Tracks { get; set; }

    public bool Equals(Albums other) => AlbumId.Equals(other.AlbumId);
}
