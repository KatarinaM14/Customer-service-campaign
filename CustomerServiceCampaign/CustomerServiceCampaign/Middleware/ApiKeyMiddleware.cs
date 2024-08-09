namespace CustomerServiceCampaign.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Integration-Api-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }

            var validApiKeys = _configuration.GetSection("ApiKeys:ValidKeys").Get<List<string>>();

            if (!validApiKeys.Contains(extractedApiKey))
            {
                context.Response.StatusCode = 403; 
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await _next(context);
        }
    }
}
