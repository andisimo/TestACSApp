﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestACSApp.Controllers
{
    public class AuthController : ApiController
    {
        public HttpResponseMessage Post()
        {
            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Add("Location", "/" + ExtractBootstrapToken());

            return response;
        }

        protected virtual string ExtractBootstrapToken()
        {
            //return HttpContext.Current.User.BootstrapToken();

            string token = null;

            // Substract the token from the bearer authorization header
            IEnumerable<string> authzHeaders;
            if (!Request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return "there was no auth header or more than 1. AuthController.ExtractBootstrapToken";
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return token;
        }

        public void Get()
        {
            int i = 0;
        }
    }
}