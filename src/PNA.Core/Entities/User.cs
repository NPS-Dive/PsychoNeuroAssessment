using PNA.Core.Enums;
using PNA.Core.ValueObjects;

namespace PNA.Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public Address? Address { get; set; }
    public JobStatus JobStatus { get; set; }
    public Cellphone? Cellphone { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? LastLogin { get; set; }

    public User ( string username, string passwordHash, string email, DateTime dateOfBirth,
        Gender gender, MaritalStatus maritalStatus, Address? address, JobStatus jobStatus,
        Cellphone? cellphone, string? imageUrl, DateTime? lastLogin )
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        Address = address;
        JobStatus = jobStatus;
        Cellphone = cellphone;
        ImageUrl = imageUrl;
        LastLogin = lastLogin;
    }
}