using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using PNA.BlazorWasm.Models;

namespace PNA.BlazorWasm.Services;

public class AuthService : IAuthService
    {
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private string _token;

    public AuthService ( HttpClient httpClient, AuthenticationStateProvider authStateProvider )
        {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authStateProvider = authStateProvider ?? throw new ArgumentNullException(nameof(authStateProvider));
        }

    public async Task<string> LoginAsync ( string email, string password )
        {
        var response = await _httpClient.PostAsJsonAsync("/auth/login", new { Email = email, Password = password });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        _token = result.Token;
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _token);
        return _token;
        }

    public async Task<UserDto> GetProfileAsync ()
        {
        var response = await _httpClient.GetFromJsonAsync<UserDto>("/auth/profile");
        return response ?? throw new InvalidOperationException("Failed to fetch profile");
        }

    public async Task UpdateProfileAsync ( string firstName, string lastName )
        {
        var response = await _httpClient.PutAsJsonAsync("/auth/profile", new { firstName, lastName });
        response.EnsureSuccessStatusCode();
        }

    public async Task<IReadOnlyList<UserDto>> ListUsersAsync ()
        {
        var users = await _httpClient.GetFromJsonAsync<List<UserDto>>("/auth/users");
        return users?.AsReadOnly() ?? throw new InvalidOperationException("Failed to fetch users");
        }

    public async Task<UserDto> GetUserAsync ( Guid id )
        {
        var user = await _httpClient.GetFromJsonAsync<UserDto>($"/auth/users/{id}");
        return user ?? throw new InvalidOperationException("Failed to fetch user");
        }

    public async Task DeleteUserAsync ( Guid id )
        {
        var response = await _httpClient.DeleteAsync($"/auth/users/{id}");
        response.EnsureSuccessStatusCode();
        }

    public async Task UpdateUserRoleAsync ( Guid id, string role )
        {
        var response = await _httpClient.PutAsJsonAsync($"/auth/users/{id}/role", $"\"{role}\""); // JSON string
        response.EnsureSuccessStatusCode();
        }

    public async Task RegisterAsync ( string userName, string email, string password, string firstName, string lastName, string role )
        {
        var response = await _httpClient.PostAsJsonAsync("/auth/register", new
            {
            UserName = userName,
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Role = role
            });
        response.EnsureSuccessStatusCode();
        }

    public async Task LogoutAsync ()
        {
        await _httpClient.PostAsync("/auth/logout", null);
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        }

    private class LoginResponse
        {
        public string Token { get; set; }
        }
    }