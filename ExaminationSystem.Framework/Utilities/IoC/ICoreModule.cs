using Microsoft.Extensions.DependencyInjection;

namespace ExaminationSystem.Framework.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}