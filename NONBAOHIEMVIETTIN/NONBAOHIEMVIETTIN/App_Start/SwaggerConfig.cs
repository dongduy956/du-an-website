using System.Web.Http;
using WebActivatorEx;
using NONBAOHIEMVIETTIN;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using System.Net.Http;

[assembly: System.Web.PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace NONBAOHIEMVIETTIN
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            string myCustomBasePath = @"https://nonbaohiem.ml";
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger("docs/{apiVersion}/swagger", c =>
                    {
                        
                        c.RootUrl((req) => myCustomBasePath);
                        c.Schemes(new[] { "https" });
                        c.PrettyPrint();
                        c.SingleApiVersion("v1", "Dương Đông Duy")
                            .Description("Api test của website")
                            .TermsOfService("Some terms")
                            .Contact(cc => cc
                                .Name("Some contact")
                                .Url("https://nonbaohiem.ml/lien-he")
                                .Email("nonbaohiemviettin@gmail.com"));
                        c.OAuth2("oauth2")
                 .Description("OAuth2 Implicit Grant")
                 .Flow("implicit")
                 .AuthorizationUrl("http://petstore.swagger.wordnik.com/api/oauth/dialog")
                 .TokenUrl("https://nonbaohiem.ml/token")
                 .Scopes(scopes =>
                 {
                     scopes.Add("read", "Read access to protected resources");
                     scopes.Add("write", "Write access to protected resources");
                 });



                    })
                .EnableSwaggerUi(c =>
                    {

                        c.EnableOAuth2Support(
                            clientId: "test-client-id",
                            clientSecret: "hahahaaha",
                            realm: "test-realm",
                            appName: "Swagger UI",
                        additionalQueryStringParams: new Dictionary<string, string>() { { "foo", "bar" } }
                        );
                        c.EnableApiKeySupport("ahahaahaha", "header");
                    });
        }


    }
}
