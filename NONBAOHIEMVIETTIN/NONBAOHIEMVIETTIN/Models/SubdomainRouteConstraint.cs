using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace NONBAOHIEMVIETTIN.Models
{
    public class SubdomainRouteConstraint : IRouteConstraint
    {
        private readonly string SubdomainWithDot;

        public SubdomainRouteConstraint(string subdomainWithDot)
        {
            SubdomainWithDot = subdomainWithDot;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return new Regex("^https?://" + SubdomainWithDot).IsMatch(httpContext.Request.Url.AbsoluteUri);
        }
    }
}