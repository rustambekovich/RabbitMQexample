using ResiverRabbitMQ.WebApi.Interfaces;

namespace ResiverRabbitMQ.WebApi.Services
{
    public class FileService : IFileService
    {
        private readonly string _path;
        public FileService(IWebHostEnvironment webEnv)
        {
            _path = Path.Combine(webEnv.WebRootPath, "MassageContener.txt");
        }
        public void Write(string text)
        {
            try
            {
                File.AppendAllText(_path, text + ", ");
            }
            catch
            {
            }
        }
    }
}
