using Microsoft.Extensions.Logging;

namespace Proprette.DataSeeding.MainService
{
    internal sealed class Default : BaseMainService
    {
        public Default(ILogger<IMainService> logger) : base(logger) { }

        protected override string ServiceName => "Default";
        protected override Task runService(string pathToDir)
        {
            return base.runService(pathToDir);
        }
    }
}
