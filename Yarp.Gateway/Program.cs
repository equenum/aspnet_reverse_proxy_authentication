using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarp.ReverseProxy.Transforms;

namespace Yarp.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
            .AddTransforms(builderContext => 
            {
                if (!string.IsNullOrEmpty(builderContext.Route.AuthorizationPolicy)) 
                {
                    builderContext.AddRequestTransform(requestContext => 
                    {
                        var userClaims = requestContext.HttpContext.User.Claims.Aggregate(
                            new Dictionary<string, string>(),
                                (dict,claim) =>
                                {
                                    dict[claim.Type] = claim.Value;
                                    return dict;
                                });

                        requestContext.ProxyRequest.Headers.Add("x-auth-json", JsonSerializer.Serialize(userClaims));
                        return ValueTask.CompletedTask;
                    });
                }
            });

        // AddReverseProxy() already adds the default authorization under the hood
        builder.Services.AddAuthentication("cookie")
            .AddCookie("cookie");

        builder.Services.AddHealthChecks();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapGet("/login/{userName}", (string userName) => Results.SignIn(
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] 
                    { 
                        new Claim("id", Guid.NewGuid().ToString()),
                        new Claim("userName", userName) 
                    },
                    "cookie"
                )
            ),
            authenticationScheme: "cookie"
        ));

        // get all the current claims for testing
        app.MapGet("/claims", (ClaimsPrincipal cp) => 
            cp.Claims.Aggregate(
                new Dictionary<string, string>(),
                (dict,claim) =>
                {
                    dict[claim.Type] = claim.Value;
                    return dict;
                }));

        app.MapReverseProxy();
        app.MapHealthChecks("health");

        app.Run();
    }
}
