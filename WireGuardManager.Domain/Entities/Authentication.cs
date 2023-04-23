using System;

namespace WireGuardManager.Domain.Entities;

public class Authentication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int IsActive { get; set; }
}