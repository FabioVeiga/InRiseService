using Microsoft.AspNetCore.Http;

namespace InRiseService.Util
{
    public static class FileHelper
    {
        private static readonly long _fileSizeLimit = 3 * 1024 * 1024; // 3 MB

        private static readonly string[] _permittedExtensions = { ".jpg", ".png", ".jpeg" };
        private static readonly Dictionary<string, string> _mimeTypes = new()
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
        };

        private static List<string> _errs = new();

        public static List<string> ValidateImage(IFormFile file)
        {
            _errs.Clear();
            if (file == null || file.Length == 0)
            {
                _errs.Add("Nenhum arquivo selecionado!");
                return _errs;
            }

            if (file.Length > _fileSizeLimit)
            {
                _errs.Add($"Arquivo excede o limite: {_fileSizeLimit} bytes.");
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
            {
                _errs.Add($"Tipo invalido: {ext}");
            }

            if (!_mimeTypes.TryGetValue(ext, out var expectedMimeType) || file.ContentType != expectedMimeType)
            {
                _errs.Add($"Tipo MIME invalido: {file.ContentType}");
            }
            return _errs;
        }
    }
}