using FCG.Games.Domain.Enums;

public record Error(
    ErrorType Type,
    string Code,
    string Message
);
