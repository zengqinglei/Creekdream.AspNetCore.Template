using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;

namespace CompanyName.ProjectName.Api.Middlewares
{
    /// <summary>
    /// Routing middleware
    /// </summary>
    public static class CustomRewriteBuilderExtensions
    {
        /// <summary>
        /// before calling .UseStaticFiles method.
        /// </summary>
        public static IApplicationBuilder UseCustomRewriter(this IApplicationBuilder app)
        {
            var options = new RewriteOptions()
                .AddRewrite(@"^((?i)api|swagger)/(.*)", "$1/$2", skipRemainingRules: true)
                .AddRewrite(@".*", "index.html", skipRemainingRules: true);

            return app.UseRewriter(options);
        }
    }
}
