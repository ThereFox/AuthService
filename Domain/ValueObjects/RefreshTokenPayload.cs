using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class RefreshTokenPayload : ValueObject
{
    public Guid UserId { get; }
    public DateTime CreatedAt { get; init; }
    public DateTime ExpiresAt { get; init; }

    private RefreshTokenPayload(Guid userId, DateTime createdAt, DateTime expiresAt)
    {
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public static Result<RefreshTokenPayload> Create(Guid userId, DateTime createAt, DateTime expiresAt)
    {
        if (createAt > expiresAt)
        {
            return Result.Failure<RefreshTokenPayload>("Invalid created at");
        }
        
        return Result.Success(new RefreshTokenPayload(userId, createAt, expiresAt));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return CreatedAt;
        yield return ExpiresAt;
    }
}