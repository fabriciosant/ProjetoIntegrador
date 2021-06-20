using LoKMais.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace LoKMais.Data.Extensions
{
    public static class DocumentoExtensions
    {
        public static byte[] ConvertToBytes(this IFormFile foto)
        {
            if (foto == null) return null;

            using var inputStream = foto.OpenReadStream();
            using var stream = new MemoryStream();
            inputStream.CopyTo(stream);

            return stream.ToArray();
        }

        public static IFormFile ConverterByteToIFormFile(this Veiculo veiculo)
        {
            if (veiculo == null) return null;
            using var stream = new MemoryStream(veiculo.Foto);
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(veiculo.Modelo.ToString()))
            {
                Headers = new HeaderDictionary(),
                ContentType = veiculo.Modelo
            };
            return file;
        }
    }
}
