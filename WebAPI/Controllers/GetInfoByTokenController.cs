using Application.UseCases;
using AuntificationService.Domain.ValueObjects;
using Domain.ValueObjects;
using Grpc.Core;

namespace WebAPI.Controllers
{
    public class GetInfoController : TokenController.TokenControllerBase
    {
        private readonly GetInfoByTokenUseCase _useCase;

        public GetInfoController(GetInfoByTokenUseCase useCase)
        {
            _useCase = useCase;
        }
        
        public override async Task<GetInfoResult> GetInfoByToken(Tokens request, ServerCallContext context)
        {
            await Task.Delay(500);

            var authToken = Token.Create(request.AuthToken);
            var refreshToken = Token.Create(request.RefreshToken);

            if(authToken.IsFailure || refreshToken.IsFailure)
            {
                var errorMessage = authToken.IsFailure ? authToken.Error : refreshToken.Error;
                
                return new GetInfoResult()
                {
                    IsSucsess = false,
                    Error = new ErrorInfo()
                    {
                        ErrorCode = "-11",
                        Message = $"invalid input: {errorMessage}"
                    }
                };
            }

            var auth = AuthorisationToken.Create(authToken.Value);
            var refresh = RefreshToken.Create(refreshToken.Value);

            if (auth.IsFailure || refresh.IsFailure)
            {
                return new GetInfoResult()
                {
                    IsSucsess = false,
                    Error = new ErrorInfo()
                    {
                        ErrorCode = "-11",
                        Message = "invalid input"
                    }
                };
            }

            var response = await _useCase.GetInfo(auth.Value, refresh.Value);

            if (response.IsFailure)
            {
                return new GetInfoResult()
                {
                    IsSucsess = response.IsSuccess,
                    Error = new ErrorInfo()
                    {
                        ErrorCode = "-5",
                        Message = response.Error
                    }
                };
            }

            else
            {
                return new GetInfoResult()
                {
                    IsSucsess = response.IsSuccess,
                    Info = new UserShortInfo()
                    {
                        Response = "Test"
                    }
                };
            }


        }
    }
}
