using BlazorPlayGround.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorPlayGround.Client.Services.ClientDifficultyService
{
    public class ClientDifficultyService : IClientDifficultyService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientDifficultyService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Difficulty> ClientDifficulty { get; set; }

        public async Task AddDifficulty(Difficulty difficulty)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/difficulty", difficulty);
            // Razor page endpoint
            _navigationManager.NavigateTo("difficultys");
        }

        public async Task DeleteDifficulty(int id)
        {
            var result = await _http.DeleteAsync($"api/difficulty/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Difficulty>? content = await result.Content.ReadFromJsonAsync<List<Difficulty>>();
                if (content != null) ClientDifficulty = content;
            }
        }

        public async Task<List<Difficulty>> GetAllDifficulty()
        {
            var result = await _http.GetFromJsonAsync<List<Difficulty>>("api/difficulty/");
            if (result != null)
            {
                ClientDifficulty = result;
            }

            return ClientDifficulty;
        }

        public async Task<Difficulty?> GetSingleDifficulty(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/difficulty/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Difficulty>();
            }
            return null;
        }

        public async Task UpdateDifficulty(int id, Difficulty request)
        {
            await _http.PutAsJsonAsync($"api/difficulty/{id}", request);
            _navigationManager.NavigateTo("difficultys");
        }
    }
}
