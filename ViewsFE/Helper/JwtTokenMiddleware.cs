namespace ViewsFE.Helper
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _request;

        public JwtTokenMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            await _request(context);
        }
    }
}
