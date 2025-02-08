namespace Application;

public record class Result<T>(T? Value, bool IsSuccess, string Message, int StatusCode = 200);