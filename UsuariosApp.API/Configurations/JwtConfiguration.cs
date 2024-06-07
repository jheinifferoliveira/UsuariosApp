﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosApp.API.Security;

namespace UsuariosApp.API.Configurations
{
    public class JwtConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            #region Adicionando a política de autenticação do projeto. 

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    //chave secreta para validar os tokens da API
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(JwtTokenSecurity.SecurityKey))
                };
            });
            #endregion
        }
    }
}
