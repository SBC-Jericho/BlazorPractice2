using BlazorPlayGround.Client.Pages;
using BlazorPlayGround.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorPlayGround.Client.Services.ClientTeamService
{
    public class ClientTeamService : IClientTeamService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientTeamService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Team> ClientTeam { get; set ; } = new List<Team>();

        public async Task AddTeam(Team team)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/team", team);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-teams");
        }

        public async Task DeleteTeam(int id)
        {
            var result = await _http.DeleteAsync($"api/team/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Team>? content = await result.Content.ReadFromJsonAsync<List<Team>>();
                if (content != null) ClientTeam = content;
            }
        }

        public async Task<List<Team>> GetAllTeam()
        {
            var result = await _http.GetFromJsonAsync<List<Team>>("api/team/");
            if (result != null)
            {
                ClientTeam = result;
            }
            return ClientTeam;
        }

        public async Task<Team?> GetSingleTeam(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/team/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Team>();
            }
            return null;
        }

        public async Task UpdateTeam(int id, Team request)
        {
            await _http.PutAsJsonAsync($"api/team/{id}", request);
            _navigationManager.NavigateTo("all-teams");
        }
    }
}
