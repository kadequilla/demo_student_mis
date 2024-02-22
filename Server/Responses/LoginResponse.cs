namespace Server.Responses;

public record LoginResponse(bool Flag, dynamic Result, string Token);