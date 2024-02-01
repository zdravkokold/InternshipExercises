using System.Threading.Tasks;
using FlurlRequests.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Flurl.Http;
using System;
using Flurl;


namespace FlurlRequests
{
    [TestFixture]
    public class FlurlTests
    {
        private const string URL = "https://localhost:60714/api/";
        private Url BASE_URL = new Url(URL);
        private const string AUTH_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs";

        [OneTimeSetUp]
        public void ClassInitialize()
        {
            FlurlHttp.GlobalSettings.Timeout = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TearDown()
        {
            RegenerateUrl();
        }               
      
        [Test]
        public async Task ContentPopulated_When_GetAlbums()
        {
            var response = await BASE_URL
                .AppendPathSegment("Albums")
                .WithOAuthBearerToken(AUTH_TOKEN)
                .GetAsync();

            response.ResponseMessage.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ContentPopulated_When_GetGenres()
        {
            var response = await BASE_URL
                .AppendPathSegment("Genres")
                .WithOAuthBearerToken(AUTH_TOKEN)
                .GetAsync();

            response.ResponseMessage.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ContentPopulated_When_GetArtists()
        {
            var response = await BASE_URL
                .AppendPathSegment("Artists")
                .WithOAuthBearerToken(AUTH_TOKEN)
                .GetAsync();

            response.ResponseMessage.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task ContentPopulated_When_GetTracks()
        {
            var response = await BASE_URL
                .AppendPathSegment("Tracks")
                .WithOAuthBearerToken(AUTH_TOKEN)
                .GetAsync();

            response.ResponseMessage.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericAlbumsById()
        {
            var responseJsonResult = await BASE_URL
                 .AppendPathSegment("Albums")
                 .AppendPathSegment(10)
                 .WithOAuthBearerToken(AUTH_TOKEN)
                 .GetStringAsync();
            var result = JsonConvert.DeserializeObject<Albums>(responseJsonResult);

            Assert.AreEqual(10, result.AlbumId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericGenresById()
        {
            var responseJsonResult = await BASE_URL
                 .AppendPathSegment("Genres")
                 .AppendPathSegment(20)
                 .WithOAuthBearerToken(AUTH_TOKEN)
                 .GetStringAsync();
            var result = JsonConvert.DeserializeObject<Genres>(responseJsonResult);

            Assert.AreEqual(20, result.GenreId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericArtistsById()
        {
            var responseJsonResult = await BASE_URL
                 .AppendPathSegment("Artists")
                 .AppendPathSegment(30)
                 .WithOAuthBearerToken(AUTH_TOKEN)
                 .GetStringAsync();
            var result = JsonConvert.DeserializeObject<Artists>(responseJsonResult);

            Assert.AreEqual(30, result.ArtistId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericTracksById()
        {
            var responseJsonResult = await BASE_URL
                 .AppendPathSegment("Tracks")
                 .AppendPathSegment(40)
                 .WithOAuthBearerToken(AUTH_TOKEN)
                 .GetStringAsync();
            var result = JsonConvert.DeserializeObject<Tracks>(responseJsonResult);

            Assert.AreEqual(40, result.TrackId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericAlbums()
        {
            var albums = await BASE_URL
                  .AppendPathSegment("Albums")
                  .WithOAuthBearerToken(AUTH_TOKEN)
                  .GetJsonAsync<List<Albums>>();

            Assert.AreEqual(347, albums.Count);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericArtists()
        {
            var artists = await BASE_URL
                  .AppendPathSegment("Artists")
                  .WithOAuthBearerToken(AUTH_TOKEN)
                  .GetJsonAsync<List<Artists>>();

            Assert.AreEqual(276, artists.Count);
        }

        [Test]
        public async Task ContentPopulated_When_PutModifiedContent()
        {
            var newGenre = await CreateUniqueGenres();

            var response = await BASE_URL.AppendPathSegments("Genres")
                                         .WithOAuthBearerToken(AUTH_TOKEN)
                                         .PostJsonAsync(newGenre);
            RegenerateUrl();

            var createdGenre = await response.GetJsonAsync<Genres>();

            string updatedName = Guid.NewGuid().ToString();
            createdGenre.Name = updatedName;

            var putResponse = await BASE_URL.AppendPathSegment("Genres")
                                    .AppendPathSegment(createdGenre.GenreId)
                                    .WithOAuthBearerToken(AUTH_TOKEN)
                                    .PutJsonAsync(createdGenre);
            RegenerateUrl();

            var actualUpdatedGenres = await BASE_URL.AppendPathSegments("Genres")
                                   .AppendPathSegment(createdGenre.GenreId)
                                   .WithOAuthBearerToken(AUTH_TOKEN)
                                   .GetJsonAsync<Genres>();
            RegenerateUrl();

            Assert.AreEqual(updatedName, actualUpdatedGenres.Name);
        }

        [Test]
        public async Task ContentPopulated_When_NewGenreInsertedViaPost()
        {
            var newGenre = await CreateUniqueGenres();

            var response = await BASE_URL.AppendPathSegment("Genres")
                                      .WithOAuthBearerToken(AUTH_TOKEN)
                                      .PostJsonAsync(newGenre);
            var responseText = await response.GetStringAsync();

            Assert.IsNotNull(responseText);
        }

        [Test]
        public async Task DataPopulated_When_NewGenreInsertedViaPost()
        {
            var newGenre = await CreateUniqueGenres();

            var response = await BASE_URL.AppendPathSegment("Genres")
                                      .WithOAuthBearerToken(AUTH_TOKEN)
                                      .PostJsonAsync(newGenre);

            var createdGenre = await response.GetJsonAsync<Genres>();

            Assert.AreEqual(newGenre.Name, createdGenre.Name);
        }

        [Test]
        public async Task DataPopulatedAsAlbums_When_NewArtistInsertedViaPost()
        {
            var newArtist = await CreateUniqueArtists();
        
            var response = await BASE_URL.AppendPathSegment("Artists")
                                      .WithOAuthBearerToken(AUTH_TOKEN)
                                      .PostJsonAsync(newArtist);
        
            var createdArtist = await response.GetJsonAsync<Artists>();
        
            Assert.AreEqual(newArtist.Name, createdArtist.Name);
        }

        [Test]
        public async Task ArtistsDeleted_When_PerformGenericDeleteRequest()
        {
            var newArtist = await CreateUniqueArtists();

            var response = await BASE_URL.AppendPathSegment("Artists")
                                       .WithOAuthBearerToken(AUTH_TOKEN)
                                       .PostJsonAsync(newArtist);
            RegenerateUrl();

            var deleteResponse = await BASE_URL.AppendPathSegments("Artists", newArtist.ArtistId)
                                       .WithOAuthBearerToken(AUTH_TOKEN)
                                       .DeleteAsync();

            deleteResponse.ResponseMessage.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task GenresDeleted_When_PerformGenericDeleteRequest()
        {
            var newGenre = await CreateUniqueGenres();

            var response = await BASE_URL.AppendPathSegment("Genres")
                                       .WithOAuthBearerToken(AUTH_TOKEN)
                                       .PostJsonAsync(newGenre);
            RegenerateUrl();

            var deleteResponse = await BASE_URL.AppendPathSegments("Genres", newGenre.GenreId)
                                       .WithOAuthBearerToken(AUTH_TOKEN)
                                       .DeleteAsync();

            deleteResponse.ResponseMessage.EnsureSuccessStatusCode();
        }

        private async Task<Artists> CreateUniqueArtists()
        {
            RegenerateUrl();
            var artists = await BASE_URL.AppendPathSegment("Artists")
                                        .WithOAuthBearerToken(AUTH_TOKEN)
                                        .GetJsonAsync<List<Artists>>();
            RegenerateUrl();

            var newArtists = new Artists
            {
                Name = Guid.NewGuid().ToString(),
                ArtistId = artists.OrderBy(x => x.ArtistId).Last().ArtistId + 1,
            };
            return newArtists;
        }

        private async Task<Genres> CreateUniqueGenres()
        {
            RegenerateUrl();
            var allGenres = await BASE_URL.AppendPathSegment("Genres")
                                  .WithOAuthBearerToken(AUTH_TOKEN)
                                  .GetJsonAsync<List<Genres>>();
            RegenerateUrl();

            var newGenre = new Genres
            {
                Name = Guid.NewGuid().ToString(),
                GenreId = allGenres.OrderBy(x => x.GenreId).Last().GenreId + 1,
            };
            return newGenre;
        }

        private void RegenerateUrl() => BASE_URL.Reset();
    }
}