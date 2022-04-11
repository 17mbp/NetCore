﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Northwind.Model;
using Northwind.UnitOfWork;
using System;
using WebApi.Authentication;
namespace WebApi.Controllers
{
    [Route("api/authenticate")]   //  [EnableCors]   DECORA CON CORS ADD CONFIGURE AND CONFIGURESERVICES
    [EnableCors("_myAllowSpecificOrigins")]
    public class TokenController : Controller
    {
        private ITokenProvider _tokenProvider;
        private IUnityOfWork _unitOfWork;
        public TokenController(ITokenProvider tokenProvider, IUnityOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public JsonWebToken Post([FromBody]User userlogin)
        {
            var user = _unitOfWork.User.ValidateUser(userlogin.Email, userlogin.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var token = new JsonWebToken
            {
                Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                Expiries_in = 480 //minutes
            };

            return token;
        }
    }
}