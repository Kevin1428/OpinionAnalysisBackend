namespace GraduationProjectBackend.Services
{
    public abstract class ServiceBase
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected ServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
