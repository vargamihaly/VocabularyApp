using VocabularyApp.Common.Core;
using System.Reflection;

namespace VocabularyApp.Presentation.Common;

public interface IApplicationVersionProvider : ISingleton
{
    string ApplicationVersion { get; }
}

public class ApplicationVersionProvider : IApplicationVersionProvider
{
    public string ApplicationVersion => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? throw new InvalidOperationException("Version cannot be determined. AssemblyInformationalVersionAttribute is not set properly.");
}
