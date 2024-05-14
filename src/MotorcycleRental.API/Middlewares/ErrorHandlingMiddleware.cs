
using Microsoft.AspNetCore.Http.HttpResults;
using MotorcycleRental.Domain.Exceptions;

namespace MotorcycleRental.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException notFound) {
				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFound.Message);

				logger.LogWarning(notFound.Message);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);

                logger.LogWarning(badRequest.Message);
            }
            catch (ForbiddenException forbidden)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
                logger.LogWarning(forbidden.Message);
            }
            catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");				
			}
        }
    }
}
