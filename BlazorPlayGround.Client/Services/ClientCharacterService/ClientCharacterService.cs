using BlazorPlayGround.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

namespace BlazorPlayGround.Client.Services.ClientCharacterService
{
    public class ClientCharacterService : IClientCharacterService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientCharacterService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Character> ClientCharacter { get; set; } = new List<Character>();

        public async Task AddCharacter(Character character)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/character/", character);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-characters");
        }

        public async Task DeleteCharacter(int id)
        {
            var result = await _http.DeleteAsync($"api/character/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Character>? content = await result.Content.ReadFromJsonAsync<List<Character>>();
                if (content != null) ClientCharacter = content;
            }
        }

        public async Task<List<Character>> GetAllCharacter()
        {
            var result = await _http.GetFromJsonAsync<List<Character>>("api/character/");
            if (result != null)
            {
                ClientCharacter = result;
            }
            return ClientCharacter;
        }

        public async Task<Character?> GetSingleCharacter(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/character/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Character>();
            }
            return null;
        }

        public async Task UpdateCharacter(int id, Character request)
        {
            await _http.PutAsJsonAsync($"api/character/{id}", request);
            _navigationManager.NavigateTo("all-characters");
        }

    }
}
