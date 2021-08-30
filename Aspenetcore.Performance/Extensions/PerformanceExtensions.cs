using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Linq;

namespace Aspnetcore.Performance.Extensions
{
    /// <summary>
    /// Extensão para uso de compressão dos requests
    /// </summary>
    public static class PerformanceExtensions
    {
        /// <summary>
        /// Comprime os requests e responses usando Gzip e Brotli
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection CompressHttpCalls(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<BrotliCompressionProviderOptions>(brotliOptions =>
            {
                brotliOptions.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(gzipOptions =>
            {
                gzipOptions.Level = CompressionLevel.Fastest;
            });

            return services;
        }

        /// <summary>
        /// Configura o json dos responses para melhor performance
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHttpJsonResponse(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opcoes =>
            {
                var serializerOptions = opcoes.JsonSerializerOptions;
                serializerOptions.IgnoreNullValues = true;
                serializerOptions.IgnoreReadOnlyProperties = true;
                serializerOptions.WriteIndented = true;
            });

            return services;
        }
    }
}
