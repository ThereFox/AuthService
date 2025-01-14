using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class RefreshTokenPayload : ValueObject
{
    public DateTime CreatedAt { get; init; }
    public DateTime ExpiresAt { get; init; }

    private RefreshTokenPayload(DateTime createdAt, DateTime expiresAt)
    {
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public static Result<RefreshTokenPayload> Create(DateTime createAt, DateTime expiresAt)
    {
        if (createAt > expiresAt)
        {
            return Result.Failure<RefreshTokenPayload>("Invalid created at");
        }
        
        return Result.Success(new RefreshTokenPayload(createAt, expiresAt));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreatedAt;
        yield return ExpiresAt;
    }
}