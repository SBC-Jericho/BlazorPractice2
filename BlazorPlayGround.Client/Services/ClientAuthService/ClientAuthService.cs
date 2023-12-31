﻿using BlazorPlayGround.Client.Pages;
using BlazorPlayGround.Shared.DTOs;
using BlazorPlayGround.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorPlayGround.Client.Services.ClientAuthService
{
    public class ClientAuthService : IClientAuthService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<User> ClientUser { get; set; } = new List<User>();
        public Token token { get ; set ; } = new Token();

        public ClientAuthService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task<List<User>> GetAllUser()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/auth/");
            if (result != null)
            {
                ClientUser = result;
            }
            return ClientUser;
        }

        public async Task<string> Login(UserLoginDTO request)
        {

            var result = await _http.PostAsJsonAsync("api/Auth/login", request);

            var data = await SetToken(result);

            return data;
        }
        public async Task Register(UserDTO request)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/auth/register", request);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-characters");
        }

        private async Task<string> SetToken(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                token.value = await message.Content.ReadAsStringAsync();
                return "success";
            }
            else
            {
                return await message.Content.ReadAsStringAsync();
            }
        }

    }
}
