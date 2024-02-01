using System.Net;
using HttpTracer;
using Examples.Models;
using Newtonsoft.Json;
using HttpTracer.Logger;
using RestSharpRequests.Models;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Serializers.NewtonsoftJson;

namespace RestSharpRequests
{
    public class RestSharpTests
    {
        private const string BASE_URL = "http://localhost:60715/";
        private static RestClient restClient;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            var options = new RestClientOptions("http://localhost:60715/")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 1000,
                FollowRedirects = true,
                MaxRedirects = 10,
                ConfigureMessageHandler = handler => new HttpTracerHandler(handler, new ConsoleLogger(), HttpMessageParts.All)
            };
            options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs",
            "Bearer");
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            restClient = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson(settings));

            restClient.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            restClient.Dispose();
        }

        [Test]
        public async Task ContentPopulated_When_GetAlbums()
        {
            var request = new RestRequest("api/Albums", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            Assert.IsNotNull(response.Content);
        }

        [Test]
        public async Task ContentPopulated_When_GetGenres()
        {
            var request = new RestRequest("api/Genres", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            Assert.IsNotNull(response.Content);
        }

        [Test]
        public async Task ContentPopulated_When_GetArtists()
        {
            var request = new RestRequest("api/Artists", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            Assert.IsNotNull(response.Content);
        }

        [Test]
        public async Task ContentPopulated_When_GetTracks()
        {
            var request = new RestRequest("api/Tracks", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            Assert.IsNotNull(response.Content);
        }

        [TestCase(1)]
        [TestCase(88)]
        [TestCase(111)]
        [TestCase(347)]
        public async Task DataPopulatedAsList_When_GetGenericAlbumsById(int id)
        {
            var request = new RestRequest($"api/Albums/{id}", Method.Get);

            var response = await restClient.ExecuteAsync<Albums>(request);

            Assert.AreEqual(id, response.Data.AlbumId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericGenresById()
        {
            var request = new RestRequest("api/Genres/15", Method.Get);

            var response = await restClient.ExecuteAsync<Genres>(request);

            Assert.AreEqual(15, response.Data.GenreId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericArtistsById()
        {
            var request = new RestRequest("api/Artists/20", Method.Get);

            var response = await restClient.ExecuteAsync<Artists>(request);

            Assert.AreEqual(20, response.Data.ArtistId);
        }

        [Test]
        public async Task DataPopulatedAsList_When_GetGenericTracksById()
        {
            var request = new RestRequest("api/Tracks/30", Method.Get);

            var response = await restClient.ExecuteAsync<Tracks>(request);

            Assert.AreEqual(30, response.Data.TrackId);
        }

        [Test]
        public async Task DataPopulatedAsGenres_When_PutModifiedContent()
        {
            var newGenres = await CreateUniqueGenres();

            var request = new RestRequest("api/Genres", Method.Post);
            request.AddJsonBody(newGenres);

            var insertedGenres = await restClient.ExecuteAsync<Genres>(request);

            var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Put);
            string updatedName = Guid.NewGuid().ToString();
            insertedGenres.Data.Name = updatedName;
            putRequest.AddJsonBody(insertedGenres.Data);

            await restClient.ExecuteAsync<Genres>(putRequest);

            request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Get);

            var getUpdatedResponse = await restClient.ExecuteAsync<Genres>(request);

            Assert.IsNotNull(getUpdatedResponse.Content);
        }

        [Test]
        public async Task DataPopulatedAsArtists_When_PutModifiedContent()
        {
            var newArtists = await CreateUniqueArtists();

            var request = new RestRequest("api/Artists", Method.Post);
            request.AddJsonBody(newArtists);

            var insertedArtists = await restClient.ExecuteAsync<Artists>(request);

            var putRequest = new RestRequest($"api/Artists/{insertedArtists.Data.ArtistId}", Method.Put);
            string updatedName = Guid.NewGuid().ToString();
            insertedArtists.Data.Name = updatedName;
            putRequest.AddJsonBody(insertedArtists.Data);

            await restClient.ExecuteAsync<Artists>(putRequest);

            request = new RestRequest($"api/Artists/{insertedArtists.Data.ArtistId}", Method.Get);

            var getUpdatedResponse = await restClient.ExecuteAsync<Genres>(request);

            Assert.IsNotNull(getUpdatedResponse.Content);
        }

        [Test]
        public async Task ContentPopulated_When_NewGenreInsertedViaPost()
        {
            var newAlbum = await CreateUniqueGenres();

            var request = new RestRequest("api/Genres");
            request.AddJsonBody(newAlbum);

            var response = await restClient.PostAsync(request);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task ContentPopulated_When_NewArtistInsertedViaPost()
        {
            var newArtist = await CreateUniqueArtists();

            var request = new RestRequest("api/Artists");
            request.AddJsonBody(newArtist);

            var response = await restClient.PostAsync(request);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task DataPopulatedAsGenres_When_NewGenreInsertedViaPost()
        {
            var newGenre = await CreateUniqueGenres();

            var request = new RestRequest("api/Genres", Method.Post);
            request.AddJsonBody(newGenre);

            var response = await restClient.ExecuteAsync<Genres>(request);

            Assert.AreEqual(newGenre.Name, response.Data.Name);
        }

        [Test]
        public async Task DataPopulatedAsArtists_When_NewArtistInsertedViaPost()
        {
            var newArtist = await CreateUniqueArtists();

            var request = new RestRequest("api/Artists", Method.Post);
            request.AddJsonBody(newArtist);

            var response = await restClient.ExecuteAsync<Artists>(request);

            Assert.AreEqual(newArtist.Name, response.Data.Name);
        }

        [Test]
        public async Task ArtistsDeleted_When_PerformDeleteRequest()
        {
            var newArtist = await CreateUniqueArtists();
            var request = new RestRequest("api/Artists", Method.Post);
            request.AddJsonBody(newArtist);
            await restClient.ExecuteAsync<Artists>(request);

            var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}", Method.Delete);
            var response = await restClient.ExecuteAsync(deleteRequest);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task ArtistsDeleted_When_PerformGenericDeleteRequest()
        {
            var newArtist = await CreateUniqueArtists();
            var request = new RestRequest("api/Artists", Method.Post);
            request.AddJsonBody(newArtist);
            await restClient.ExecuteAsync<Artists>(request);

            var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}", Method.Delete);
            var response = await restClient.ExecuteAsync<Artists>(deleteRequest);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task GenresDeleted_When_PerformGenericDeleteRequest()
        {
            var newGenre = await CreateUniqueGenres();
            var request = new RestRequest("api/Genres", Method.Post);
            request.AddJsonBody(newGenre);
            await restClient.ExecuteAsync<Genres>(request);

            var deleteRequest = new RestRequest($"api/Genres/{newGenre.GenreId}", Method.Delete);
            var response = await restClient.ExecuteAsync<Genres>(deleteRequest);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public async Task ValidateGetAlbumsJsonSchema()
        {
            var request = new RestRequest("api/Albums", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            string jsonSchemaContent = @"
            {
              ""$schema"": ""http://json-schema.org/draft-06/schema#"",
              ""type"": ""array"",
              ""items"": {
                ""type"": ""object"",
                ""properties"": {
                  ""albumId"": {
                    ""type"": ""integer""
                  },
                  ""title"": {
                    ""type"": ""string""
                  },
                  ""artistId"": {
                    ""type"": ""integer""
                  },
                  ""artist"": {
                    ""type"": [""object"", ""null""]
                  },
                  ""tracks"": {
                    ""type"": ""array""
                  }
                },
                ""required"": [""albumId"", ""title"", ""artistId"", ""artist"", ""tracks""],
                ""additionalProperties"": false
              }
            }";

            response.AssertSchema(jsonSchemaContent);
        }

        [Test]
        public async Task ValidateGetTracksJsonSchema()
        {
            var request = new RestRequest("api/Tracks", Method.Get);
            var response = await restClient.ExecuteAsync(request);

            string jsonSchemaContent = @"
            {
             ""$schema"": ""http://json-schema.org/draft-07/schema#"",
             ""type"": ""array"",
             ""items"": {
               ""type"": ""object"",
               ""properties"": {
                 ""trackId"": { ""type"": ""integer"" },
                 ""name"": { ""type"": ""string"" },
                 ""albumId"": { ""type"": ""integer"" },
                 ""mediaTypeId"": { ""type"": ""integer"" },
                 ""genreId"": { ""type"": ""integer"" },
                 ""composer"": { ""type"": [""string"", ""null""] },
                 ""milliseconds"": { ""type"": ""integer"" },
                 ""bytes"": { ""type"": ""integer"" },
                 ""unitPrice"": { ""type"": ""string"", ""pattern"": ""^\\d+(\\.\\d{1,2})?$"" },
                 ""album"": { ""type"": [""object"", ""null""] },
                 ""genre"": { ""type"": [""object"", ""null""] },
                 ""mediaType"": { ""type"": [""object"", ""null""] },
                 ""invoiceItems"": { ""type"": ""array"" },
                 ""playlistTrack"": { ""type"": ""array"" }
               },
               ""required"": [""trackId"", ""name"", ""albumId"", ""mediaTypeId"", ""genreId"", ""milliseconds"", ""bytes"", ""unitPrice"", ""album"", ""genre"", ""mediaType"", ""invoiceItems"", ""playlistTrack""],
               ""additionalProperties"": false
             }
           }";

            response.AssertSchema(jsonSchemaContent);
        }

        private async Task<Artists> CreateUniqueArtists()
        {
            var artists = await restClient.GetAsync<List<Artists>>(new RestRequest("api/Artists"));
            var newArtists = new Artists
            {
                Name = Guid.NewGuid().ToString(),
                ArtistId = artists.OrderBy(x => x.ArtistId).Last().ArtistId + 1,
            };
            return newArtists;
        }

        private async Task<Genres> CreateUniqueGenres()
        {
            var genres = await restClient.GetAsync<List<Genres>>(new RestRequest("api/Genres"));
            var newGenres = new Genres
            {
                Name = Guid.NewGuid().ToString(),
                GenreId = genres.OrderBy(x => x.GenreId).Last().GenreId + 1,
            };
            return newGenres;
        }
    }
}