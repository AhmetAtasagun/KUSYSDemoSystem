using System.Reflection;

namespace KUSYS.Business.Infrastructure
{
    public class BusinessAssembly
    {
        public static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
    }
}
