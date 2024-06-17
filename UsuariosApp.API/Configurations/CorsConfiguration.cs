namespace UsuariosApp.API.Configurations
{
    public class CorsConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Política de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5104","http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        public static void Use(IApplicationBuilder app)
        {
            //Política de CORS
            app.UseCors("DefaultPolicy");
        }
    }
}
